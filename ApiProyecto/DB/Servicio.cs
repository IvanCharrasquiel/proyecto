using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServicio { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreServicio { get; set; }  // Cambiado a PascalCase

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Range(0, double.MaxValue)]  // Asegurando que el precio sea no negativo
        public float Precio { get; set; }  // Cambiado a PascalCase

        [Range(0, int.MaxValue)]  // Asegurando que los minutos sean no negativos
        public int Minutos { get; set; }  // Cambiado a PascalCase

        // Relación con Categoria
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public virtual Categoria Categoria { get; set; }  // Propiedad de navegación

        public virtual ICollection<Promocion> Promociones { get; set; }

        // Relación de muchos a muchos
        public virtual ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();

        // Relación de muchos a muchos con Servicio a través de ComboServicio
        public virtual ICollection<ComboServicio> ComboServicios { get; set; }

    }
}
