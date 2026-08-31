using Microsoft.EntityFrameworkCore;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using NominaApp.Infrastructure.Data;

namespace NominaApp.Infrastructure.Repositories;

public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
{
    public EmpleadoRepository(NominaDbContext context) : base(context) { }

    public async Task<IEnumerable<Empleado>> GetByDepartamentoAsync(int departamentoId) =>
        await _dbSet.Where(e => e.DepartamentoId == departamentoId).ToListAsync();

    public async Task<Empleado?> GetByCedulaAsync(string cedula) =>
        await _dbSet.FirstOrDefaultAsync(e => e.Cedula == cedula);

    public async Task<Empleado?> GetWithContratoActivoAsync(int empleadoId) =>
        await _dbSet
            .Include(e => e.Contratos.Where(c => c.Activo))
            .FirstOrDefaultAsync(e => e.Id == empleadoId);
}