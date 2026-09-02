namespace NominaApp.API.DTOs
{
    public class NominaDto
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
        public List<DetalleNominaDto> Detalles { get; set; } = new();
    }
}
