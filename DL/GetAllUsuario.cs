using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class GetAllUsuario
    {
        
        
            public int IdUsuario { get; set; }
            public string? UsuarioNombre { get; set; }
            public string? ApellidoPaterno { get; set; }
            public string? ApellidoMaterno { get; set; }
            public string? Correo { get; set; }
            public string? Contraseña { get; set; }
            public string? Telefono { get; set; }
            public DateOnly? FechaNacimiento { get; set; }
            public byte[]? Imagen { get; set; }
            public int IdRol { get; set; }
            public string? RolNombre { get; set; }
            public int IdGenero { get; set; }
            public string? GeneroNombre { get; set; }
            public string? Calle { get; set; }
            public string? NumeroExterior { get; set; }
            public string? NumeroInterior { get; set; }
            public int? IdColonia { get; set; }
            public string? ColoniaNombre { get; set; }
            public string? CodigoPostal { get; set; }
            public int? IdMunicipio { get; set; }
            public string? MunicipioNombre { get; set; }
            public int? IdEstado { get; set; }
            public string? EstadoNombre { get; set; }
        
    }
}

