


namespace NominaApp.API.DTOs
{
    public class DetalleNominaDto
    {
        public string Concepto { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // "Deduccion" / "Ingreso"
        public decimal Monto { get; set; }
    }
}
