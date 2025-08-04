using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? Correo { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? Contraseña { get; set; }

        public string? Telefono { get; set; }
        public string? FechaNacimiento { get; set; }
        public int? IdGenero { get; set; }
        public int? IdRol { get; set; }
        public Genero? Genero { get; set; }
        public Rol? Rol { get; set; }
        
        public List<object>? Usuarios { get; set; }
        public Direccion? Direccion { get; set; }
    }
}
