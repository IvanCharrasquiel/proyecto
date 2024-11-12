namespace models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string? NombreServicio { get; set; }

    public string? Descripcion { get; set; }

    public double? Precio { get; set; }

    public int? Duracion { get; set; }

    public virtual ICollection<Promocion> Promocions { get; set; } = new List<Promocion>();

    public virtual ICollection<ServicioReserva> ServicioReservas { get; set; } = new List<ServicioReserva>();
}
