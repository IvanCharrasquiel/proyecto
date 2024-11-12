namespace Peluqueria.Model;

public partial class ServicioReserva
{
    public int IdServicioReserva { get; set; }

    public int? IdReserva { get; set; }

    public int? IdServicio { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
