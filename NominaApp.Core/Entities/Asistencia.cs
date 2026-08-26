using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Asistencia
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public TipoAsistencia Tipo { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
