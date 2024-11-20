using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Servicioreserva
{
    public int IdServicioReserva { get; set; }

    public int IdReserva { get; set; }

    public int IdServicio { get; set; }

    public virtual ICollection<Detallefactura> Detallefacturas { get; set; } = new List<Detallefactura>();

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
