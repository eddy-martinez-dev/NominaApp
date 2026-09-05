

using NominaApp.Core.Entities;

namespace NominaApp.Core.Interfaces;

public interface IComprobanteService
{
    byte[] GenerarPdf(Nomina nomina, string nombreEmpleado);
    string GuardarEnDisco(byte[] pdfBytes, int nominaId);
}