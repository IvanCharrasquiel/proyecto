namespace models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public int? IdPersona { get; set; }

    public string? Contraseña { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
