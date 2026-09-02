using Microsoft.AspNetCore.Mvc;
using NominaApp.API.DTOs;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;

namespace NominaApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly IDepartamentoRepository _repository;

    public DepartamentoController(IDepartamentoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> GetAll()
    {
        var departamentos = await _repository.GetAllAsync();
        return Ok(departamentos.Select(ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartamentoDto>> GetById(int id)
    {
        var departamento = await _repository.GetByIdAsync(id);
        if (departamento is null) return NotFound();
        return Ok(ToDto(departamento));
    }

    [HttpPost]
    public async Task<ActionResult<DepartamentoDto>> Create([FromBody] CrearDepartamentoRequest request)
    {
        var departamento = new Departamento
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        await _repository.AddAsync(departamento);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = departamento.Id }, ToDto(departamento));
    }

    private static DepartamentoDto ToDto(Departamento d) => new()
    {
        Id = d.Id,
        Nombre = d.Nombre,
        Descripcion = d.Descripcion,
        ResponsableId = d.ResponsableId
    };
}