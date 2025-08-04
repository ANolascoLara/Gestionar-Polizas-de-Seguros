using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using ML;

namespace BL
{
    public class Municipio
    {
        private readonly SistemaGestionPolizaContext _context;
        public Municipio(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var municipios = _context.Municipios
                        .Where(m => m.IdEstado == IdEstado)
                        .ToList();



                if (municipios.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in municipios)
                    {
                        ML.Municipio municipio = new ML.Municipio();
                        municipio.IdMunicipio = item.IdMunicipio;
                        municipio.NombreMunicipio = item.Nombre;

                        result.Objects.Add(municipio);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay municipios";
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
