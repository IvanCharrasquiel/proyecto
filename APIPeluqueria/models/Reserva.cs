namespace models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public DateOnly? Fecha { get; set; }

    public DateTime? HoraInicio { get; set; }

    public DateTime? HoraFin { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEmpleado { get; set; }

    public string? EstadoReserva { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual ICollection<ServicioReserva> ServicioReservas { get; set; } = new List<ServicioReserva>();
}
