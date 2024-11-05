namespace ApiProyecto.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly? FechaEmision { get; set; }

    public decimal? MontoTotal { get; set; }

    public int? IdCita { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Citum? IdCitaNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
