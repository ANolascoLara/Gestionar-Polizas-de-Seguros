using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class Colonia
    {
        private readonly SistemaGestionPolizaContext _context;
        public Colonia(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var municipios = _context.Colonia
                        .Where(m => m.IdMunicipio == IdMunicipio)
                        .ToList();



                if (municipios.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in municipios)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.IdColonia = item.IdColonia;
                        colonia.NombreColonia = item.Nombre;

                        result.Objects.Add(colonia);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay colonias";
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
