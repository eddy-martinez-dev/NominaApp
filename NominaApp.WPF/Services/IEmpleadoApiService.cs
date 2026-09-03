

using NominaApp.WPF.Models;

namespace NominaApp.WPF.Services;

public interface IEmpleadoApiService
{
    Task<List<EmpleadoModel>> GetAllAsync();
    Task<EmpleadoModel?> GetByIdAsync(int id);
}