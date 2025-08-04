using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class Login
    {
        private readonly SistemaGestionPolizaContext _context;
        public Login(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result LoggIn(ML.Login login)
        {
            ML.Result result = new ML.Result();

            try
            {
                var resultado = (from usuario in _context.Usuarios
                                 join role in _context.Rols on usuario.IdRol equals role.IdRol
                                 where usuario.Correo == login.Email && usuario.Contraseña == login.Password
                                 select new
                                 {
                                     IdUsuario = usuario.IdUsuario,
                                     Nombre = usuario.Nombre,
                                     ApellidoPaterno = usuario.ApellidoPaterno,
                                     ApellidoMaterno = usuario.ApellidoMaterno,
                                     NombreRol = role.Nombre
                                 }).FirstOrDefault();

                if (resultado != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.IdUsuario = resultado.IdUsuario;
                    usuario.NombreUsuario = resultado.Nombre;
                    usuario.ApellidoPaterno = resultado.ApellidoPaterno;
                    usuario.ApellidoMaterno = resultado.ApellidoMaterno;

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.NombreRol = resultado.NombreRol;

                    result.Object = usuario;
                    result.Correct = true;
                } else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Correo o contraseña son incorrectos";
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
