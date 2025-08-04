using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class TipoPoliza
    {
        private readonly SistemaGestionPolizaContext _context;

        public TipoPoliza(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var tipoPolizas = _context.TipoPolizas.ToList();

                if(tipoPolizas.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in tipoPolizas)
                    {
                        ML.TipoPoliza tipoPoliza = new ML.TipoPoliza();
                        tipoPoliza.IdTipoPoliza = item.IdTipoPoliza;
                        tipoPoliza.Nombre = item.Nombre;

                        result.Objects.Add(tipoPoliza);
                    }

                    result.Correct = true;
                } else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay tipo polizas";
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
