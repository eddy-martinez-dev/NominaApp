using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Contrato
    {
        public int Id { get; set; }
        public TipoContrato TipoContrato { get; set; }
        public decimal SalarioBase { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Activo { get; set; } = true;

        // Relación
        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
