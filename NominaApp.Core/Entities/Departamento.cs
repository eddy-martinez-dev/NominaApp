using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // Jefe de departamento (opcional)
        public int? ResponsableId { get; set; }
        public Empleado? Responsable { get; set; }

        // Navegación
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
