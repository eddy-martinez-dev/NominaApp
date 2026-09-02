using Microsoft.AspNetCore.Mvc;
using NominaApp.API.DTOs;
using NominaApp.Core.Interfaces;

namespace NominaApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NominaController : ControllerBase
{
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly INominaRepository _nominaRepository;
    private readonly ICalculadorNomina _calculadorNomina;

    public NominaController(
        IEmpleadoRepository empleadoRepository,
        INominaRepository nominaRepository,
        ICalculadorNomina calculadorNomina)
    {
        _empleadoRepository = empleadoRepository;
        _nominaRepository = nominaRepository;
        _calculadorNomina = calculadorNomina;
    }

    [HttpPost("generar")]
    public async Task<ActionResult<NominaDto>> Generar([FromBody] GenerarNominaRequest request)
    {
        // 1. Buscar empleado con su contrato activo
        var empleado = await _empleadoRepository.GetWithContratoActivoAsync(request.EmpleadoId);
        if (empleado is null)
            return NotFound($"No se encontró el empleado con Id {request.EmpleadoId}.");

        var contratoActivo = empleado.Contratos.FirstOrDefault(c => c.Activo);
        if (contratoActivo is null)
            return BadRequest("El empleado no tiene un contrato activo.");

        // 2. Evitar generar nómina duplicada para el mismo período
        var nominaExistente = await _nominaRepository.GetByEmpleadoYPeriodoAsync(request.EmpleadoId, request.Periodo);
        if (nominaExistente is not null)
            return Conflict("Ya existe una nómina generada para este empleado en este período.");

        // 3. Por ahora, sin asistencias ni conceptos (los agregamos cuando tengamos esos endpoints)
        var asistencias = new List<Core.Entities.Asistencia>();
        var conceptos = new List<Core.Entities.ConceptoNomina>();

        // 4. Calcular (aquí se usa el Result pattern)
        var resultado = _calculadorNomina.Calcular(empleado, contratoActivo, asistencias, conceptos, request.Periodo);

        if (!resultado.EsExitoso)
            return BadRequest(resultado.Error);

        // 5. Guardar
        var nomina = resultado.Valor!;
        await _nominaRepository.AddAsync(nomina);
        await _nominaRepository.SaveChangesAsync();

        // 6. Mapear a DTO y devolver
        var dto = NominaMapper.ToDto(nomina, $"{empleado.Nombre} {empleado.Apellido}");
        return CreatedAtAction(nameof(ObtenerPorId), new { id = nomina.Id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NominaDto>> ObtenerPorId(int id)
    {
        var nomina = await _nominaRepository.GetWithDetallesAsync(id);
        if (nomina is null)
            return NotFound();

        var empleado = await _empleadoRepository.GetByIdAsync(nomina.EmpleadoId);
        var nombreEmpleado = empleado is not null ? $"{empleado.Nombre} {empleado.Apellido}" : "Desconocido";

        return Ok(NominaMapper.ToDto(nomina, nombreEmpleado));
    }
}