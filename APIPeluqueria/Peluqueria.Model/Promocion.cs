namespace Peluqueria.Model;

public partial class Promocion
{
    public int IdPromocion { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFinal { get; set; }

    public double? Descuento { get; set; }

    public string? Estado { get; set; }

    public int? IdServicio { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
