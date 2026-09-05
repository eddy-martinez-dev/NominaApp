using NominaApp.WPF.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace NominaApp.WPF.Services;

public class NominaApiService : INominaApiService
{
    private readonly HttpClient _httpClient;

    public NominaApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool EsExitoso, NominaModel? Nomina, string? Error)> GenerarAsync(GenerarNominaModel request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/nomina/generar", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, null, error);
        }

        var nomina = await response.Content.ReadFromJsonAsync<NominaModel>();
        return (true, nomina, null);
    }

    public async Task<NominaModel?> ObtenerPorIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/nomina/{id}");
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<NominaModel>();
    }

    public async Task<byte[]?> DescargarComprobanteAsync(int nominaId)
    {
        var response = await _httpClient.GetAsync($"api/nomina/{nominaId}/comprobante");
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadAsByteArrayAsync();
    }
}