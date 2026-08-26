using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class DetalleNomina
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }

        public int NominaId { get; set; }
        public Nomina? Nomina { get; set; }

        public int ConceptoNominaId { get; set; }
        public ConceptoNomina? ConceptoNomina { get; set; }
    }
}
