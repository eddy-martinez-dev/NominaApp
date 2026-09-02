using Microsoft.AspNetCore.Mvc;
using NominaApp.API.DTOs;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;

namespace NominaApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpleadoController : ControllerBase
{
    private readonly IEmpleadoRepository _repository;

    public EmpleadoController(IEmpleadoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetAll()
    {
        var empleados = await _repository.GetAllAsync();
        return Ok(empleados.Select(ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmpleadoDto>> GetById(int id)
    {
        var empleado = await _repository.GetByIdAsync(id);
        if (empleado is null) return NotFound();
        return Ok(ToDto(empleado));
    }

    [HttpPost]
    public async Task<ActionResult<EmpleadoDto>> Create([FromBody] CrearEmpleadoRequest request)
    {
        var existente = await _repository.GetByCedulaAsync(request.Cedula);
        if (existente is not null)
            return Conflict("Ya existe un empleado registrado con esa cédula.");

        var empleado = new Empleado
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Cedula = request.Cedula,
            FechaNacimiento = request.FechaNacimiento,
            FechaContratacion = request.FechaContratacion,
            Email = request.Email,
            Telefono = request.Telefono,
            Direccion = request.Direccion,
            DepartamentoId = request.DepartamentoId,
            PuestoId = request.PuestoId
        };

        await _repository.AddAsync(empleado);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, ToDto(empleado));
    }

    private static EmpleadoDto ToDto(Empleado e) => new()
    {
        Id = e.Id,
        Nombre = e.Nombre,
        Apellido = e.Apellido,
        Cedula = e.Cedula,
        Email = e.Email,
        Estado = e.Estado.ToString(),
        DepartamentoId = e.DepartamentoId,
        PuestoId = e.PuestoId
    };
}