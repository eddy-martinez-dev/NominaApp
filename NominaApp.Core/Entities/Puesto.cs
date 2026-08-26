using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Puesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal SalarioMinimo { get; set; }
        public decimal SalarioMaximo { get; set; }

        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
