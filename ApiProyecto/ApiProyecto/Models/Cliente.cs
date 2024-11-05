namespace ApiProyecto.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public int? IdPersona { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
}
