using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? ApellidoPaterno { get; set; }

    public byte[]? Imagen { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public string? Telefono { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public int? IdRol { get; set; }

    public int? IdGenero { get; set; }

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual Genero? IdGeneroNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<PolizaUsuario> PolizaUsuarios { get; set; } = new List<PolizaUsuario>();
}
