namespace Peluqueria.DTO
{
    public class ClienteDTO : PersonaDTO
    {
        public DateOnly? FechaRegistro { get; set; }
        public string? Contraseña { get; set; }
        public List<ReservaDTO> Reservas { get; set; } = new List<ReservaDTO>(); // Navegación para mostrar historial de reservas
    }
}
