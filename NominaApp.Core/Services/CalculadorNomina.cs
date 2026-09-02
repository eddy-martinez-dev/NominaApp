using NominaApp.Core.Common;
using NominaApp.Core.Entities;
using NominaApp.Core.Enums;
using NominaApp.Core.Interfaces;

namespace NominaApp.Core.Services;

public class CalculadorNomina : ICalculadorNomina
{
    public Result<Nomina> Calcular(Empleado empleado, Contrato contratoActivo,
        IEnumerable<Asistencia> asistenciasDelPeriodo,
        IEnumerable<ConceptoNomina> conceptosAplicables,
        DateTime periodo)
    {
        // Validaciones primero (Result pattern en acción)
        if (contratoActivo is null || !contratoActivo.Activo)
            return Result<Nomina>.Failure("El empleado no tiene un contrato activo.");

        if (contratoActivo.SalarioBase <= 0)
            return Result<Nomina>.Failure("El salario base del contrato debe ser mayor a cero.");

        decimal salarioBruto = contratoActivo.TipoContrato == TipoContrato.PorHoras
            ? CalcularPorHoras(contratoActivo, asistenciasDelPeriodo)
            : contratoActivo.SalarioBase;

        if (salarioBruto <= 0)
            return Result<Nomina>.Failure("El salario bruto calculado no puede ser cero o negativo.");

        var detalles = new List<DetalleNomina>();
        decimal totalDeducciones = 0;
        decimal totalIngresosAdicionales = 0;

        foreach (var concepto in conceptosAplicables)
        {
            decimal monto = concepto.EsPorcentaje
                ? salarioBruto * (concepto.Valor / 100)
                : concepto.Valor;

            detalles.Add(new DetalleNomina
            {
                ConceptoNominaId = concepto.Id,
                Monto = monto
            });

            if (concepto.Tipo == TipoConcepto.Deduccion)
                totalDeducciones += monto;
            else
                totalIngresosAdicionales += monto;
        }

        var nomina = new Nomina
        {
            EmpleadoId = empleado.Id,
            Periodo = periodo,
            SalarioBruto = salarioBruto,
            TotalDeducciones = totalDeducciones,
            TotalIngresosAdicionales = totalIngresosAdicionales,
            SalarioNeto = salarioBruto - totalDeducciones + totalIngresosAdicionales,
            FechaGeneracion = DateTime.UtcNow,
            Detalles = detalles
        };

        return Result<Nomina>.Success(nomina);
    }

    private decimal CalcularPorHoras(Contrato contrato, IEnumerable<Asistencia> asistencias)
    {
        decimal totalHoras = asistencias.Sum(a => a.HorasTrabajadas);
        return totalHoras * contrato.SalarioBase; // aquí SalarioBase = tarifa por hora
    }
}