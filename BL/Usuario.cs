using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario : IUsuario
    {
        private readonly DL.SistemaGestionPolizaContext _context;

        public Usuario(DL.SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {

                var usuarios = _context.GetAllUsuarios.FromSqlRaw("EXEC UsuarioGetAll").ToList();

                result.Objects = new List<object>();

                foreach (var u in usuarios)
                {
                    ML.Usuario usuariobd = new ML.Usuario
                    {
                        IdUsuario = u.IdUsuario,
                        NombreUsuario = u.UsuarioNombre,
                        ApellidoPaterno = u.ApellidoPaterno,
                        ApellidoMaterno = u.ApellidoMaterno,
                        Correo = u.Correo,
                        Contraseña = u.Contraseña,
                        Telefono = u.Telefono,
                        FechaNacimiento = u.FechaNacimiento,
                        Imagen = u.Imagen,

                        Rol = new ML.Rol
                        {
                            IdRol = u.IdRol,
                            NombreRol = u.RolNombre
                        },

                        Genero = new ML.Genero
                        {
                            IdGenero = u.IdGenero,
                            NombreGenero = u.GeneroNombre
                        },


                        Direccion = (u.Calle != null || u.NumeroExterior != null || u.NumeroInterior != null ||
                                     u.ColoniaNombre != null || u.CodigoPostal != null ||
                                     u.MunicipioNombre != null || u.EstadoNombre != null)
                                    ? new ML.Direccion
                                    {
                                        Calle = u.Calle,
                                        NumeroExterior = u.NumeroExterior,
                                        NumeroInterior = u.NumeroInterior,

                                        Colonia = (u.ColoniaNombre != null || u.CodigoPostal != null)
                                                    ? new ML.Colonia
                                                    {
                                                        IdColonia = u.IdColonia.GetValueOrDefault(),
                                                        NombreColonia = u.ColoniaNombre,
                                                        CodigoPostal = u.CodigoPostal,
                                                    } : null,

                                        Municipio = u.MunicipioNombre != null
                                                    ? new ML.Municipio
                                                    {
                                                        IdMunicipio = u.IdMunicipio.GetValueOrDefault(),
                                                        NombreMunicipio = u.MunicipioNombre
                                                    } : null,

                                        Estado = u.EstadoNombre != null
                                                    ? new ML.Estado
                                                    {
                                                        IdEstado = u.IdEstado.GetValueOrDefault(),
                                                        NombreEstado = u.EstadoNombre
                                                    } : null
                                    } : null
                    };

                    result.Objects.Add(usuariobd);
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
                    result.Object = new ML.Usuario
                    {
                        IdUsuario = usuarioDL.IdUsuario,
                        NombreUsuario = usuarioDL.UsuarioNombre,
                        ApellidoPaterno = usuarioDL.ApellidoPaterno,
                        ApellidoMaterno = usuarioDL.ApellidoMaterno,
                        Correo = usuarioDL.Correo,
                        Contraseña = usuarioDL.Contraseña,
                        Telefono = usuarioDL.Telefono,
                        FechaNacimiento = usuarioDL.FechaNacimiento,
                        Imagen = usuarioDL.Imagen,

                        Rol = new ML.Rol
                        {
                            IdRol = usuarioDL.IdRol,
                            NombreRol = usuarioDL.RolNombre
                        },
                        Genero = new ML.Genero
                        {
                            IdGenero = usuarioDL.IdGenero,
                            NombreGenero = usuarioDL.GeneroNombre
                        },
                        Direccion = (usuarioDL.Calle != null || usuarioDL.NumeroExterior != null || usuarioDL.NumeroInterior != null ||
                                     usuarioDL.ColoniaNombre != null || usuarioDL.CodigoPostal != null ||
                                     usuarioDL.MunicipioNombre != null || usuarioDL.EstadoNombre != null)
                                    ? new ML.Direccion
                                    {
                                        Calle = usuarioDL.Calle,
                                        NumeroExterior = usuarioDL.NumeroExterior,
                                        NumeroInterior = usuarioDL.NumeroInterior,
                                        Colonia = (usuarioDL.ColoniaNombre != null || usuarioDL.CodigoPostal != null)
                                                    ? new ML.Colonia
                                                    {
                                                        IdColonia = usuarioDL.IdColonia.GetValueOrDefault(),
                                                        NombreColonia = usuarioDL.ColoniaNombre,
                                                        CodigoPostal = usuarioDL.CodigoPostal,
                                                    } : null,
                                        Municipio = usuarioDL.MunicipioNombre != null
                                                    ? new ML.Municipio
                                                    {
                                                        IdMunicipio = usuarioDL.IdMunicipio.GetValueOrDefault(),
                                                        NombreMunicipio = usuarioDL.MunicipioNombre
                                                    } : null,
                                        Estado = usuarioDL.EstadoNombre != null
                                                    ? new ML.Estado
                                                    {
                                                        IdEstado = usuarioDL.IdEstado.GetValueOrDefault(),
                                                        NombreEstado = usuarioDL.EstadoNombre
                                                    } : null
                                    } : null
                    };
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


    }
}
