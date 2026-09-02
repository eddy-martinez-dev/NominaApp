


using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using NominaApp.Infrastructure.Data;

namespace NominaApp.Infrastructure.Repositories;

public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
{
    public DepartamentoRepository(NominaDbContext context) : base(context) { }
}