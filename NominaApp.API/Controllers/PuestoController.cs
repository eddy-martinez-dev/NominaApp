using Microsoft.AspNetCore.Mvc;
using NominaApp.API.DTOs;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;

namespace NominaApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuestoController : ControllerBase
{
    private readonly IPuestoRepository _repository;

    public PuestoController(IPuestoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PuestoDto>>> GetAll()
    {
        var puestos = await _repository.GetAllAsync();
        return Ok(puestos.Select(ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PuestoDto>> GetById(int id)
    {
        var puesto = await _repository.GetByIdAsync(id);
        if (puesto is null) return NotFound();
        return Ok(ToDto(puesto));
    }

    [HttpPost]
    public async Task<ActionResult<PuestoDto>> Create([FromBody] CrearPuestoRequest request)
    {
        var puesto = new Puesto
        {
            Nombre = request.Nombre,
            SalarioMinimo = request.SalarioMinimo,
            SalarioMaximo = request.SalarioMaximo
        };

        await _repository.AddAsync(puesto);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = puesto.Id }, ToDto(puesto));
    }

    private static PuestoDto ToDto(Puesto p) => new()
    {
        Id = p.Id,
        Nombre = p.Nombre,
        SalarioMinimo = p.SalarioMinimo,
        SalarioMaximo = p.SalarioMaximo
    };
}