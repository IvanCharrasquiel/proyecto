// DTO/ServicioDTO.cs
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProyectoO.DTO
{
    public class ServicioDTO
    {
        [JsonProperty("IdServicio")]
        public int IdServicio { get; set; }

        [JsonProperty("NombreServicio")]
        public string NombreServicio { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Precio")]
        public decimal Precio { get; set; }

        [JsonProperty("Duracion")]
        public int Duracion { get; set; } // Duración en minutos

        [JsonProperty("Promociones")]
        public List<PromocionDTO> Promociones { get; set; } = new List<PromocionDTO>();
    }
}
