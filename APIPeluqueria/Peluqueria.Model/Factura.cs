namespace Peluqueria.Model
{
    public partial class Factura
    {
        public int IdFactura { get; set; }
        public string NumeroDocumeto { get; set; }
        public DateOnly? FechaEmision { get; set; }
        public decimal? MontoTotal { get; set; }
        public int? IdReserva { get; set; }
        
        // Agrega la propiedad Estado para indicar el estado de la factura
        public string? Estado { get; set; }

        public virtual Reserva? IdReservaNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
