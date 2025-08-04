using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class PolizaDTO
    {
        public int NumeroPoliza { get; set; }
        public int IdTipoPoliza { get; set; }

        public string? TipoPolizaNombre { get; set; }


        public int IdEstatus { get; set; }
        public int IdUsuario { get; set; }
        public string? EstatusNombre { get; set; }

        public string? FechaInicio { get; set; }  // formato 105: dd-mm-yyyy
        public string? FechaFin { get; set; }

        public decimal? MontoPrima { get; set; }

    }



}
