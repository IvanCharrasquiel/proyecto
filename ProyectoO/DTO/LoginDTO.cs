
using Newtonsoft.Json;

namespace ProyectoO.DTO
{
    public class LoginDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("contraseña")]
        public string Contraseña { get; set; }
    }
}
