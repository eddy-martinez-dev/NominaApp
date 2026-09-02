

using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using NominaApp.Infrastructure.Data;

namespace NominaApp.Infrastructure.Repositories;

public class PuestoRepository : Repository<Puesto>, IPuestoRepository
{
    public PuestoRepository(NominaDbContext context) : base(context) { }
}