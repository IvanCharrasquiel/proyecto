using Newtonsoft.Json;

namespace ProyectoO.DTO
{
    public class ReservaRequestDTO
    {
        [JsonProperty("Fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("HoraInicio")]
        public TimeSpan HoraInicio { get; set; }

        [JsonProperty("HoraFin")]
        public TimeSpan HoraFin { get; set; }

        [JsonProperty("IdCliente")]
        public int IdCliente { get; set; }

        [JsonProperty("IdEmpleado")]
        public int IdEmpleado { get; set; }

        [JsonProperty("Servicios")]
        public List<int> Servicios { get; set; } // Lista de IdServicio
    }
}