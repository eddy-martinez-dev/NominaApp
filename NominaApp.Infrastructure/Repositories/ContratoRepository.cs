

using Microsoft.EntityFrameworkCore;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using NominaApp.Infrastructure.Data;

namespace NominaApp.Infrastructure.Repositories;

public class ContratoRepository : Repository<Contrato>, IContratoRepository
{
    public ContratoRepository(NominaDbContext context) : base(context) { }

    public async Task<Contrato?> GetActivoByEmpleadoAsync(int empleadoId) =>
        await _dbSet.FirstOrDefaultAsync(c => c.EmpleadoId == empleadoId && c.Activo);
}