using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public EstadoEmpleado Estado { get; set; } = EstadoEmpleado.Activo;

        // Relaciones (FK)
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

        public int PuestoId { get; set; }
        public Puesto? Puesto { get; set; }

        // Navegación (colecciones)
        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
        public ICollection<Nomina> Nominas { get; set; } = new List<Nomina>();
        public Usuario? Usuario { get; set; }
    }
}
