using NominaApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Entities
{
    public class ConceptoNomina
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TipoConcepto Tipo { get; set; }
        public bool EsPorcentaje { get; set; }
        public decimal Valor { get; set; }

        public ICollection<DetalleNomina> DetallesNomina { get; set; } = new List<DetalleNomina>();
    }
}
