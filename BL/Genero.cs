using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class Genero
    {
        private readonly SistemaGestionPolizaContext _context;

        public Genero(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var tipoPolizas = _context.Generos.ToList();

                if (tipoPolizas.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in tipoPolizas)
                    {
                        ML.Genero genero = new ML.Genero();
                        genero.IdGenero = item.IdGenero;
                        genero.NombreGenero = item.Nombre;

                        result.Objects.Add(genero);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay tipo polizas";
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
