namespace Peluqueria.Model
{
    public partial class Cliente : Persona
    {
        public DateOnly? FechaRegistro { get; set; }
        public string? Contraseña { get; set; } // Contraseña del cliente

        // Relación con Reserva y Pago (Ejemplo de relación adicional)
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
