using NominaApp.WPF.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace NominaApp.WPF.Services;

public class EmpleadoApiService : IEmpleadoApiService
{
    private readonly HttpClient _httpClient;

    public EmpleadoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<EmpleadoModel>> GetAllAsync()
    {
        var resultado = await _httpClient.GetFromJsonAsync<List<EmpleadoModel>>("api/empleado");
        return resultado ?? new List<EmpleadoModel>();
    }

    public async Task<EmpleadoModel?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/empleado/{id}");
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<EmpleadoModel>();
    }
}