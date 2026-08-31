

using NominaApp.Core.Entities;

namespace NominaApp.Core.Interfaces;

public interface IEmpleadoRepository : IRepository<Empleado>
{
    Task<IEnumerable<Empleado>> GetByDepartamentoAsync(int departamentoId);
    Task<Empleado?> GetByCedulaAsync(string cedula);
    Task<Empleado?> GetWithContratoActivoAsync(int empleadoId); // incluye Contrato vía Include
}