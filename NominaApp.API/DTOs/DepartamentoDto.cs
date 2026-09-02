namespace NominaApp.API.DTOs;

public class DepartamentoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int? ResponsableId { get; set; }
}

public class CrearDepartamentoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}