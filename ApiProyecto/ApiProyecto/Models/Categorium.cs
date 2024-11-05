namespace ApiProyecto.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
