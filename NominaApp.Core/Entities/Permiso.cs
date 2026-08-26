using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Permiso
    {
        public int Id { get; set; }
        public TipoPermiso Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasSolicitados { get; set; }
        public EstadoPermiso Estado { get; set; } = EstadoPermiso.Pendiente;

        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
