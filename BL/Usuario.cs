using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Usuario
    {
        private readonly DL.SistemaGestionPolizaContext _context;

        public Usuario(DL.SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {

                var usuarios = _context.GetAllUsuarios.FromSqlRaw("UsuarioGetAll").ToList();

                result.Objects = new List<object>();

                foreach (var u in usuarios)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Genero = new ML.Genero();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    // Datos personales
                    usuario.IdUsuario = u.IdUsuario;
                    usuario.NombreUsuario = u.UsuarioNombre;
                    usuario.ApellidoPaterno = u.ApellidoPaterno;
                    usuario.ApellidoMaterno = u.ApellidoMaterno;
                    usuario.Correo = u.Correo;
                    usuario.Contraseña = u.Contraseña;
                    usuario.Telefono = u.Telefono;
                    usuario.FechaNacimiento = u.FechaNacimiento;
                    usuario.Imagen = u.Imagen;

                    // Rol
                    usuario.Rol.IdRol = u.IdRol;
                    usuario.Rol.NombreRol = u.RolNombre;

                    // Género
                    usuario.Genero.IdGenero = u.IdGenero;
                    usuario.Genero.NombreGenero = u.GeneroNombre;

                    // Dirección
                    usuario.Direccion.Calle = u.Calle;
                    usuario.Direccion.NumeroExterior = u.NumeroExterior;
                    usuario.Direccion.NumeroInterior = u.NumeroInterior;

                    usuario.Direccion.Colonia.IdColonia = u.IdColonia ?? 0;
                    usuario.Direccion.Colonia.NombreColonia = u.ColoniaNombre;
                    usuario.Direccion.Colonia.CodigoPostal = u.CodigoPostal;

                    usuario.Direccion.Colonia.Municipio.IdMunicipio = u.IdMunicipio ?? 0;
                    usuario.Direccion.Colonia.Municipio.NombreMunicipio = u.MunicipioNombre;

                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = u.IdEstado ?? 0;
                    usuario.Direccion.Colonia.Municipio.Estado.NombreEstado = u.EstadoNombre;

                    result.Objects.Add(usuario);
                }


                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al obtener los usuarios.";
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var rowsAffected = _context.Database.ExecuteSqlRaw("EXEC UsuarioDelete @IdUsuario", new Microsoft.Data.SqlClient.SqlParameter("@IdUsuario", IdUsuario));

                if (rowsAffected > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo eliminar el usuario.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al eliminar el usuario.";
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var usuarioDL = _context.GetAllUsuarios
                                     .FromSqlRaw("EXEC UsuarioGetById @IdUsuario",
                                         new Microsoft.Data.SqlClient.SqlParameter("@IdUsuario", IdUsuario))
                                     .AsEnumerable()
                                     .FirstOrDefault();

                if (usuarioDL != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.IdUsuario = usuarioDL.IdUsuario;
                    usuario.NombreUsuario = usuarioDL.UsuarioNombre;
                    usuario.ApellidoPaterno = usuarioDL.ApellidoPaterno;
                    usuario.ApellidoMaterno = usuarioDL.ApellidoMaterno;
                    usuario.Correo = usuarioDL.Correo;
                    usuario.Contraseña = usuarioDL.Contraseña;
                    usuario.Telefono = usuarioDL.Telefono;
                    usuario.FechaNacimiento = usuarioDL.FechaNacimiento;
                    //usuario.Imagen = usuarioDL.Imagen;
                    usuario.ImagenBase64 = usuarioDL.Imagen != null ? Convert.ToBase64String(usuarioDL.Imagen) : "";


                    usuario.Rol = new ML.Rol();

                    usuario.Rol.IdRol = usuarioDL.IdRol;
                    usuario.Rol.NombreRol = usuarioDL.RolNombre;

                    usuario.Genero = new ML.Genero();

                    usuario.Genero.IdGenero = usuarioDL.IdGenero;
                    usuario.Genero.NombreGenero = usuarioDL.GeneroNombre;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = usuarioDL.Calle;
                    usuario.Direccion.NumeroExterior = usuarioDL.NumeroExterior;
                    usuario.Direccion.NumeroInterior = usuarioDL.NumeroInterior;

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = usuarioDL.IdColonia ?? 0;
                    usuario.Direccion.Colonia.NombreColonia = usuarioDL.ColoniaNombre;
                    usuario.Direccion.Colonia.CodigoPostal = usuarioDL.CodigoPostal;

                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.IdMunicipio = usuarioDL.IdMunicipio ?? 0;
                    usuario.Direccion.Colonia.Municipio.NombreMunicipio = usuarioDL.MunicipioNombre;

                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = usuarioDL.IdEstado ?? 0;
                    usuario.Direccion.Colonia.Municipio.Estado.NombreEstado = usuarioDL.EstadoNombre;

                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = usuarioDL.IdPais.Value;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.NombrePais = usuarioDL.PaisNombre;

                    result.Object = usuario;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Usuario no encontrado.";
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al obtener el usuario por ID.";
                result.Ex = ex;
            }

            return result;
        }



        public ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@Nombre", usuario.NombreUsuario),
                new SqlParameter("@ApellidoPaterno", usuario.ApellidoPaterno),
                new SqlParameter("@ApellidoMaterno", usuario.ApellidoMaterno),
                new SqlParameter("@Correo", usuario.Correo),
                new SqlParameter("@Contraseña", usuario.Contraseña),
                new SqlParameter("@Telefono", usuario.Telefono),
                new SqlParameter("@FechaNacimiento", usuario.FechaNacimiento),
                new SqlParameter("@Imagen", usuario.Imagen ?? (object)DBNull.Value),
                new SqlParameter("@IdRol", usuario.Rol.IdRol),
                new SqlParameter("@IdGenero", usuario.Genero.IdGenero),
                new SqlParameter("@Calle", usuario.Direccion.Calle),
                new SqlParameter("@NumeroExterior", usuario.Direccion.NumeroExterior),
                new SqlParameter("@NumeroInterior", usuario.Direccion.NumeroInterior ?? (object)DBNull.Value),
                new SqlParameter("@IdColonia", usuario.Direccion.Colonia.IdColonia)
            };

                int filasAfectadas = _context.Database.ExecuteSqlRaw(
                    "EXEC UsuarioAdd @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Correo, @Contraseña, @Telefono, @FechaNacimiento, @Imagen, @IdRol, @IdGenero, @Calle, @NumeroExterior, @NumeroInterior, @IdColonia",
                    parameters);

                result.Correct = filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@Nombre", usuario.NombreUsuario),
                new SqlParameter("@ApellidoPaterno", usuario.ApellidoPaterno),
                new SqlParameter("@ApellidoMaterno", usuario.ApellidoMaterno),
                new SqlParameter("@Correo", usuario.Correo),
                new SqlParameter("@Contraseña", usuario.Contraseña),
                new SqlParameter("@Telefono", usuario.Telefono),
                new SqlParameter("@FechaNacimiento", usuario.FechaNacimiento),
                new SqlParameter("@Imagen", usuario.Imagen ?? (object)DBNull.Value),
                new SqlParameter("@IdRol", usuario.Rol.IdRol),
                new SqlParameter("@IdGenero", usuario.Genero.IdGenero),
                new SqlParameter("@Calle", usuario.Direccion.Calle),
                new SqlParameter("@NumeroExterior", usuario.Direccion.NumeroExterior),
                new SqlParameter("@NumeroInterior", usuario.Direccion.NumeroInterior ?? (object)DBNull.Value),
                new SqlParameter("@IdColonia", usuario.Direccion.Colonia.IdColonia)
            };

                int filasAfectadas = _context.Database.ExecuteSqlRaw(
                    "EXEC UsuarioUpdate @IdUsuario, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Correo, @Contraseña, @Telefono, @FechaNacimiento, @Imagen, @IdRol, @IdGenero, @Calle, @NumeroExterior, @NumeroInterior, @IdColonia",
                    parameters);

                result.Correct = filasAfectadas > 0;
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
