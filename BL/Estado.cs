using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using ML;

namespace BL
{
    public class Estado
    {
        private readonly SistemaGestionPolizaContext _context;
        public Estado(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();

            try
            {
                var estados = _context.Estados
                    .Where(e => e.IdPais == idPais)
                    .ToList();


                if (estados.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in estados)
                    {
                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = item.IdEstado;
                        estado.NombreEstado = item.Nombre;

                        result.Objects.Add(estado);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay Estados";
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
