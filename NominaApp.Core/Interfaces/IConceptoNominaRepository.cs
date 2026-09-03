


using NominaApp.Core.Entities;

namespace NominaApp.Core.Interfaces;

public interface IConceptoNominaRepository : IRepository<ConceptoNomina>
{
    Task<IEnumerable<ConceptoNomina>> GetActivosAsync(); 
}