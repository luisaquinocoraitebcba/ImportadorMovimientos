using System;
using System.Collections.Generic;

namespace ImportadorMovimientos.Data.Entities;

public partial class Cliente
{
    public int Id { get; set; }

    public string Dni { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
