// DTO/RegistroRequestDTO.cs
using Newtonsoft.Json;

namespace ProyectoO.DTO
{
    public class RegistroRequestDTO
    {
        [JsonProperty("contraseña")]
        public string Contraseña { get; set; }

        [JsonProperty("persona")]
        public PersonaDTO Persona { get; set; }
    }
}
