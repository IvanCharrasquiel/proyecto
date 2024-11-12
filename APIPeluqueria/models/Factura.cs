namespace models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly? FechaEmision { get; set; }

    public decimal? MontoTotal { get; set; }

    public int? IdReserva { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
