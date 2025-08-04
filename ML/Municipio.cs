using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        public int IdMunicipio { get; set; }
        public string? NombreMunicipio { get; set; }

        public Estado? Estado { get; set; }

    }
}
