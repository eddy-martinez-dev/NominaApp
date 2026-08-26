using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Nomina
    {
        public int Id { get; set; }
        public DateTime Periodo { get; set; } // primer día del mes que representa
        public decimal SalarioBruto { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal TotalIngresosAdicionales { get; set; }
        public decimal SalarioNeto { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public EstadoNomina Estado { get; set; } = EstadoNomina.Generada;

        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }

        public ICollection<DetalleNomina> Detalles { get; set; } = new List<DetalleNomina>();
    }
}
