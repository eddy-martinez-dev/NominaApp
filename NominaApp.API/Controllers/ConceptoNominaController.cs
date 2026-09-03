using Microsoft.AspNetCore.Mvc;
using NominaApp.API.DTOs;
using NominaApp.Core.Entities;
using NominaApp.Core.Enums;
using NominaApp.Core.Interfaces;

namespace NominaApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConceptoNominaController : ControllerBase
{
    private readonly IConceptoNominaRepository _repository;

    public ConceptoNominaController(IConceptoNominaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConceptoNominaDto>>> GetAll()
    {
        var conceptos = await _repository.GetAllAsync();
        return Ok(conceptos.Select(ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConceptoNominaDto>> GetById(int id)
    {
        var concepto = await _repository.GetByIdAsync(id);
        if (concepto is null) return NotFound();
        return Ok(ToDto(concepto));
    }

    [HttpPost]
    public async Task<ActionResult<ConceptoNominaDto>> Create([FromBody] CrearConceptoNominaRequest request)
    {
        if (!Enum.TryParse<TipoConcepto>(request.Tipo, out var tipo))
            return BadRequest("Tipo inválido. Usa 'Deduccion' o 'Ingreso'.");

        var concepto = new ConceptoNomina
        {
            Nombre = request.Nombre,
            Tipo = tipo,
            EsPorcentaje = request.EsPorcentaje,
            Valor = request.Valor
        };

        await _repository.AddAsync(concepto);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = concepto.Id }, ToDto(concepto));
    }

    private static ConceptoNominaDto ToDto(ConceptoNomina c) => new()
    {
        Id = c.Id,
        Nombre = c.Nombre,
        Tipo = c.Tipo.ToString(),
        EsPorcentaje = c.EsPorcentaje,
        Valor = c.Valor
    };
}