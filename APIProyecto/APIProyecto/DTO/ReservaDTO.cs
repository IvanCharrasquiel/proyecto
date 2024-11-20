using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace APIProyecto.DTO
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int IdEmpleado { get; set; }
        public string EstadoReserva { get; set; } = "Pendiente";
        public List<int> Servicios { get; set; } = new List<int>();
    }
}