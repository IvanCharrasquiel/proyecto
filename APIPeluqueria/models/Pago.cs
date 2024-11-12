namespace models;

public partial class Pago
{
    public int IdPago { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public string? Estado { get; set; }

    public int? IdFactura { get; set; }

    public int? IdMetodoPago { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }
}
