namespace NominaApp.WPF.Models;

public class NominaModel
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }
    public string NombreEmpleado { get; set; } = string.Empty;
    public DateTime Periodo { get; set; }
    public decimal SalarioBruto { get; set; }
    public decimal TotalDeducciones { get; set; }
    public decimal TotalIngresosAdicionales { get; set; }
    public decimal SalarioNeto { get; set; }
    public string Estado { get; set; } = string.Empty;
    public List<DetalleNominaModel> Detalles { get; set; } = new();
}