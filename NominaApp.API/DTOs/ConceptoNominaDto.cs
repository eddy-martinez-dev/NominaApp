namespace NominaApp.API.DTOs;

public class ConceptoNominaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // "Deduccion" | "Ingreso"
    public bool EsPorcentaje { get; set; }
    public decimal Valor { get; set; }
}

public class CrearConceptoNominaRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public bool EsPorcentaje { get; set; }
    public decimal Valor { get; set; }
}