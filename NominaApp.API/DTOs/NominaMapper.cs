using NominaApp.Core.Entities;

namespace NominaApp.API.DTOs;

public static class NominaMapper
{
    public static NominaDto ToDto(Nomina nomina, string nombreEmpleado)
    {
        return new NominaDto
        {
            Id = nomina.Id,
            EmpleadoId = nomina.EmpleadoId,
            NombreEmpleado = nombreEmpleado,
            Periodo = nomina.Periodo,
            SalarioBruto = nomina.SalarioBruto,
            TotalDeducciones = nomina.TotalDeducciones,
            TotalIngresosAdicionales = nomina.TotalIngresosAdicionales,
            SalarioNeto = nomina.SalarioNeto,
            Estado = nomina.Estado.ToString(),
            Detalles = nomina.Detalles.Select(d => new DetalleNominaDto
            {
                Concepto = d.ConceptoNomina?.Nombre ?? string.Empty,
                Tipo = d.ConceptoNomina?.Tipo.ToString() ?? string.Empty,
                Monto = d.Monto
            }).ToList()
        };
    }
}