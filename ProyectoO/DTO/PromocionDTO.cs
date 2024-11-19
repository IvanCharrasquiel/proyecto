// DTO/PromocionDTO.cs
using Newtonsoft.Json;

namespace ProyectoO.DTO
{
    public class PromocionDTO
    {
        [JsonProperty("IdPromocion")]
        public int IdPromocion { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Descuento")]
        public decimal Descuento { get; set; }

        // Otros campos según tu base de datos
    }
}
