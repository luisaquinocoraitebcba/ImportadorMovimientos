using System;
using System.Collections.Generic;

namespace ImportadorMovimientos.Data.Entities;

public partial class Movimiento
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int TipoMovimientoId { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Importe { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual TiposMovimiento TipoMovimiento { get; set; } = null!;
}
