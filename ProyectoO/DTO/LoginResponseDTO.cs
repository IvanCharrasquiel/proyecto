
using Newtonsoft.Json;

namespace ProyectoO.DTO
{
    public class LoginResponseDTO
    {
        [JsonProperty("role")]
        public string Role { get; set; } // "Empleado" o "Cliente"

        [JsonProperty("id")]
        public int Id { get; set; } // IdEmpleado o IdCliente

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("idPersona")]
        public int IdPersona { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; } // Token JWT
    }
}
