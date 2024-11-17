// ProyectoO/DTO/ReservaDTO.cs
namespace ProyectoO.DTO
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }
        public int IdPersona { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Servicio { get; set; }
        // Propiedad para mostrar en la UI
        public string FechaHora { get; set; }
    }
}
