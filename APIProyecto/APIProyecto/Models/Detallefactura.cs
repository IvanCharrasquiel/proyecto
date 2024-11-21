using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Detallefactura
{
    public int IdDetalleFactura { get; set; }

    public decimal Subtotal { get; set; }

    public decimal PrecioServicio { get; set; }

    public int CantidadServicio { get; set; }

    public int IdFactura { get; set; }

    public int IdServicioReserva { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual Servicioreserva IdServicioReservaNavigation { get; set; } = null!;
}
