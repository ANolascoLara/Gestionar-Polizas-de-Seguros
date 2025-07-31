using System;
using System.Collections.Generic;

namespace DL;

public partial class Estatus
{
    public int IdEstatus { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
