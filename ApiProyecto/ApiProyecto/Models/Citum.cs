namespace ApiProyecto.Models;

public partial class Citum
{
    public int IdCita { get; set; }

    public int? IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdHorario { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
