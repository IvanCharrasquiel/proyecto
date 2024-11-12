namespace models;

public partial class DetalleFactura
{
    public int IdDetalleFactura { get; set; }

    public decimal? Subtotal { get; set; }

    public int? PrecioServicio { get; set; }

    public int IdFactura { get; set; }

    public int? CantidadServicio { get; set; }

    public int? IdServicio { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual ServicioReserva? IdServicioNavigation { get; set; }
}
