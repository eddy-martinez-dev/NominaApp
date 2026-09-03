

using NominaApp.WPF.Models;

namespace NominaApp.WPF.Services;

public interface INominaApiService
{
    Task<(bool EsExitoso, NominaModel? Nomina, string? Error)> GenerarAsync(GenerarNominaModel request);
    Task<NominaModel?> ObtenerPorIdAsync(int id);
}