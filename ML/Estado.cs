using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        public int IdEstado { get; set; }
        public string? NombreEstado { get; set; }

        public int IdPais { get; set; }
        public Pais? Pais { get; set; }

        
        public ICollection<Municipio>? Municipios { get; set; }
    }
}
