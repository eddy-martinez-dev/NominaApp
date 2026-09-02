namespace NominaApp.API.DTOs;

public class PuestoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal SalarioMinimo { get; set; }
    public decimal SalarioMaximo { get; set; }
}

public class CrearPuestoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public decimal SalarioMinimo { get; set; }
    public decimal SalarioMaximo { get; set; }
}