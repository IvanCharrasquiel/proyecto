
using Newtonsoft.Json;

namespace ProyectoO.DTO
{
    public class PersonaDTO
    {
        [JsonProperty("idPersona")]
        public int? IdPersona { get; set; } 

        [JsonProperty("cedula")]
        public int? Cedula { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("telefono")]
        public string? Telefono { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("direccion")]
        public string? Direccion { get; set; }

        [JsonProperty("fotoPerfil")]
        public string? FotoPerfil { get; set; }
    }
}
