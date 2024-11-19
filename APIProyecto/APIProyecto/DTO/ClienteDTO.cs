using APIProyecto.Models;

namespace APIProyecto.DTO
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int IdPersona { get; set; }
        public PersonaDTO Persona { get; set; } = null!;
    }
}
