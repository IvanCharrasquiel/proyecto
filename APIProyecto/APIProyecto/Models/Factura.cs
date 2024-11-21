using APIProyecto.Models;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Factura
{
    public int IdFactura { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public DateTime? FechaEmision { get; set; }

    public decimal? MontoTotal { get; set; }

    public string Estado { get; set; }

    public int IdReserva { get; set; }

    public int IdCliente { get; set; }

    public virtual ICollection<Detallefactura> Detallefacturas { get; set; } = new List<Detallefactura>();

    [ForeignKey(nameof(IdCliente))]
    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
