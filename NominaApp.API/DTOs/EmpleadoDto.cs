namespace NominaApp.API.DTOs;

public class EmpleadoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int DepartamentoId { get; set; }
    public int PuestoId { get; set; }
}

public class CrearEmpleadoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public DateTime FechaContratacion { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public int DepartamentoId { get; set; }
    public int PuestoId { get; set; }
}