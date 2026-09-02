using NominaApp.Core.Common;
using NominaApp.Core.Entities;

namespace NominaApp.Core.Interfaces;

public interface ICalculadorNomina
{
    Result<Nomina> Calcular(Empleado empleado, Contrato contratoActivo,
        IEnumerable<Asistencia> asistenciasDelPeriodo,
        IEnumerable<ConceptoNomina> conceptosAplicables,
        DateTime periodo);
}