using System;
using System.Collections.Generic;

namespace DL;

public partial class TipoPoliza
{
    public int IdTipoPoliza { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
