

using NominaApp.Core.Entities;

namespace NominaApp.Core.Interfaces;

public interface INominaRepository : IRepository<Nomina>
{
    Task<IEnumerable<Nomina>> GetByEmpleadoAsync(int empleadoId);
    Task<Nomina?> GetByEmpleadoYPeriodoAsync(int empleadoId, DateTime periodo);
    Task<Nomina?> GetWithDetallesAsync(int nominaId); // incluye DetallesNomina + ConceptoNomina
}