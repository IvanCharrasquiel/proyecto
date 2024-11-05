namespace ApiProyecto.Models;

public partial class CitaServicio
{
    public int? IdCita { get; set; }

    public int? IdServicio { get; set; }

    public virtual Citum? IdCitaNavigation { get; set; }
}
