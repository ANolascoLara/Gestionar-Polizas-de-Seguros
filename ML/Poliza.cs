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
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public decimal? MontoPrima { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdEstatus { get; set; }
        public int? IdtipoPoliza { get; set; }
        public TipoPoliza? TipoPoliza { get; set; }

    }
}
