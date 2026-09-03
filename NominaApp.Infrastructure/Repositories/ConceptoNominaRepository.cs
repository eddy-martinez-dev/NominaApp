using Microsoft.EntityFrameworkCore;
using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using NominaApp.Infrastructure.Data;

namespace NominaApp.Infrastructure.Repositories;

public class ConceptoNominaRepository : Repository<ConceptoNomina>, IConceptoNominaRepository
{
    public ConceptoNominaRepository(NominaDbContext context) : base(context) { }

    public async Task<IEnumerable<ConceptoNomina>> GetActivosAsync() =>
        await _dbSet.ToListAsync();
}