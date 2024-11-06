using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Servicio
    {
        public int IdServicio { get; set; }

        public string Servicio1 { get; set; }

        public string Descripcion { get; set; }

        public double? Precio { get; set; }

        public int? Minutos { get; set; }

        public int? IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

        // Relación muchos a muchos con Cita a través de CitaServicio
        public virtual ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();

        // Relación muchos a muchos con Cita a través de ComboServicio
        public virtual ICollection<ComboServicio> ComboServicios { get; set; } = new List<ComboServicio>();

    }
}
