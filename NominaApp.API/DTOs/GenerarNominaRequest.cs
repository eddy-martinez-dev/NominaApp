


namespace NominaApp.API.DTOs
{
    public class GenerarNominaRequest
    {
        public int EmpleadoId { get; set; }
        public DateTime Periodo { get; set; } // ej. 2026-09-01 representa "septiembre 2026"
    }
}
