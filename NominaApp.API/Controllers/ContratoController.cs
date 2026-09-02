using Microsoft.AspNetCore.Mvc;
using NominaApp.API.DTOs;
using NominaApp.Core.Entities;
using NominaApp.Core.Enums;
using NominaApp.Core.Interfaces;

namespace NominaApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratoController : ControllerBase
{
    private readonly IContratoRepository _repository;
    private readonly IEmpleadoRepository _empleadoRepository;

    public ContratoController(IContratoRepository repository, IEmpleadoRepository empleadoRepository)
    {
        _repository = repository;
        _empleadoRepository = empleadoRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContratoDto>> GetById(int id)
    {
        var contrato = await _repository.GetByIdAsync(id);
        if (contrato is null) return NotFound();
        return Ok(ToDto(contrato));
    }

    [HttpPost]
    public async Task<ActionResult<ContratoDto>> Create([FromBody] CrearContratoRequest request)
    {
        var empleado = await _empleadoRepository.GetByIdAsync(request.EmpleadoId);
        if (empleado is null)
            return NotFound($"No se encontró el empleado con Id {request.EmpleadoId}.");

        if (!Enum.TryParse<TipoContrato>(request.TipoContrato, out var tipo))
            return BadRequest("TipoContrato inválido. Usa 'Fijo', 'Temporal' o 'PorHoras'.");

        // Si ya tiene un contrato activo, lo desactivamos (un empleado solo debería tener 1 activo)
        var contratoActivo = await _repository.GetActivoByEmpleadoAsync(request.EmpleadoId);
        if (contratoActivo is not null)
        {
            contratoActivo.Activo = false;
            _repository.Update(contratoActivo);
        }

        var contrato = new Contrato
        {
            EmpleadoId = request.EmpleadoId,
            TipoContrato = tipo,
            SalarioBase = request.SalarioBase,
            FechaInicio = request.FechaInicio,
            FechaFin = request.FechaFin,
            Activo = true
        };

        await _repository.AddAsync(contrato);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = contrato.Id }, ToDto(contrato));
    }

    private static ContratoDto ToDto(Contrato c) => new()
    {
        Id = c.Id,
        EmpleadoId = c.EmpleadoId,
        TipoContrato = c.TipoContrato.ToString(),
        SalarioBase = c.SalarioBase,
        FechaInicio = c.FechaInicio,
        FechaFin = c.FechaFin,
        Activo = c.Activo
    };
}