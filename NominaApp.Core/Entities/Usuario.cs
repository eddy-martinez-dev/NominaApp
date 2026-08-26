using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }

        // Opcional: no todo usuario tiene que ser empleado (ej. un Admin externo)
        public int? EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
