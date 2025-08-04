using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Poliza
    {
        public int? NumeroPoliza {  get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFinal { get; set; }
        public decimal? MontoPrima { get; set; }
        public ML.Usuario? Usuario { get; set; }
        public ML.Estatus? Estatus { get; set; }
        public TipoPoliza? TipoPoliza { get; set; }

        public List<object>? Polizas { get; set; }

    }
}
