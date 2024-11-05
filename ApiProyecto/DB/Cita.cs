using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCita { get; set; }

        public int? IdCliente { get; set; }

        public int? IdEmpleado { get; set; }

        public DateTime? Fecha { get; set; }

        public int? IdHorario { get; set; }


        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        public virtual Cliente? IdClienteNavigation { get; set; }

    }

}
