namespace NominaApp.API.DTOs;

public class ContratoDto
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }
    public string TipoContrato { get; set; } = string.Empty;
    public decimal SalarioBase { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool Activo { get; set; }
}

public class CrearContratoRequest
{
    public int EmpleadoId { get; set; }
    public string TipoContrato { get; set; } = string.Empty; // "Fijo" | "Temporal" | "PorHoras"
    public decimal SalarioBase { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}