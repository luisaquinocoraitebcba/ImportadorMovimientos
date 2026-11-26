using System;
using System.Collections.Generic;

namespace ImportadorMovimientos.Data.Entities;

public partial class TiposMovimiento
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
