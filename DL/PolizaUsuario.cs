using System;
using System.Collections.Generic;

namespace DL;

public partial class PolizaUsuario
{
    public int IdPolizaUsuario { get; set; }

    public int? IdUsuario { get; set; }

    public int? NumeroPoliza { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual Poliza? NumeroPolizaNavigation { get; set; }
}
