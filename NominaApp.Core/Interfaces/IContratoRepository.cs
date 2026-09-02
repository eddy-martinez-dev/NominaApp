

using NominaApp.Core.Entities;

namespace NominaApp.Core.Interfaces;

public interface IContratoRepository : IRepository<Contrato>
{
    Task<Contrato?> GetActivoByEmpleadoAsync(int empleadoId);
}