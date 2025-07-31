using System;
using System.Collections.Generic;

namespace DL;

public partial class Poliza
{
    public int NumeroPoliza { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? Montoprima { get; set; }

    public int? IdTipoPoliza { get; set; }

    public int? IdEstatus { get; set; }

    public virtual Estatus? IdEstatusNavigation { get; set; }

    public virtual TipoPoliza? IdTipoPolizaNavigation { get; set; }
}
