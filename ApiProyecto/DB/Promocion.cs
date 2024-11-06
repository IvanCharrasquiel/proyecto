using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Promocion
    {
        public int IdPromocion { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

        public double Descuento { get; set; }

        public string Estado { get; set; }

        public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();
    }
}
