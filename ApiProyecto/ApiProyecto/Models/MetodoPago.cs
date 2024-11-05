namespace ApiProyecto.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string? MetodoPago1 { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
