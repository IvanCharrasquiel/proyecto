namespace ApiProyecto.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string? Servicio1 { get; set; }

    public string? Descripcion { get; set; }

    public double? Precio { get; set; }

    public int? Minutos { get; set; }

    public int IdCategoria { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
