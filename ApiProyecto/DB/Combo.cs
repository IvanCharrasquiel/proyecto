using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Combo
    {
        public int IdCombo { get; set; }

        public double? Precio { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int? DuracionTotal { get; set; }

        public int? IdPromocion { get; set; }

        public virtual Promocion IdPromocionNavigation { get; set; } = null!;

        public virtual ICollection<ComboServicio> ComboServicios { get; set; } = new List<ComboServicio>();
    }
}
