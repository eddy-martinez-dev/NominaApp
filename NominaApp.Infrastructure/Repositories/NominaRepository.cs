using Microsoft.EntityFrameworkCore;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using NominaApp.Infrastructure.Data;

namespace NominaApp.Infrastructure.Repositories;

public class NominaRepository : Repository<Nomina>, INominaRepository
{
    public NominaRepository(NominaDbContext context) : base(context) { }

    public async Task<IEnumerable<Nomina>> GetByEmpleadoAsync(int empleadoId) =>
        await _dbSet.Where(n => n.EmpleadoId == empleadoId).ToListAsync();

    public async Task<Nomina?> GetByEmpleadoYPeriodoAsync(int empleadoId, DateTime periodo) =>
        await _dbSet.FirstOrDefaultAsync(n => n.EmpleadoId == empleadoId && n.Periodo == periodo);

    public async Task<Nomina?> GetWithDetallesAsync(int nominaId) =>
        await _dbSet
            .Include(n => n.Detalles)
                .ThenInclude(d => d.ConceptoNomina)
            .FirstOrDefaultAsync(n => n.Id == nominaId);
}