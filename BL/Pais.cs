using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class Pais
    {
        private readonly SistemaGestionPolizaContext _context;
        public Pais(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.Pais.ToList();

                if(query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Pais pais = new ML.Pais();
                        pais.IdPais = item.IdPais;
                        pais.NombrePais = item.Nombre;

                        result.Objects.Add(pais);
                    }

                    result.Correct = true;
                } else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay paises";
                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
