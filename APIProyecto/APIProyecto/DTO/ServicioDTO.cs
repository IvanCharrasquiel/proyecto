using APIProyecto.Models;
using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class ServicioDTO
    {
        public int IdServicio { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreServicio { get; set; } = null!;

        [StringLength(500)]
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }

        public  int Duracion { get; set; }
        public virtual ICollection<PromocionDTO> Promociones { get; set; } = new List<PromocionDTO>();
    }
}
