namespace ApiProyecto.Models;

public partial class Promocion
{
    public int IdPromocion { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFinal { get; set; }

    public double? Descuento { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();
}
