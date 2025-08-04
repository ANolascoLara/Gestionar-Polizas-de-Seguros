using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        private readonly DL.SistemaGestionPolizaContext _context;

        public Rol(DL.SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var roles = _context.Rols.ToList();

                result.Objects = new List<object>();

                foreach (var item in roles)
                {
                    ML.Rol rol = new ML.Rol
                    {
                        IdRol = item.IdRol,
                        NombreRol = item.Nombre
                    };

                    result.Objects.Add(rol);
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Error al consultar los roles.";
                result.Ex = ex;
            }

            return result;
        }

    }
}
