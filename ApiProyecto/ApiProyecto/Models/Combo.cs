namespace ApiProyecto.Models;

public partial class Combo
{
    public int IdCombo { get; set; }

    public double? Precio { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? DuracionTotal { get; set; }

    public int IdPromocion { get; set; }

    public virtual Promocion IdPromocionNavigation { get; set; } = null!;
}
