using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public partial class Empleado
    {
        public Empleado()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        public double? Comision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaContrato { get; set; }

        public int? IdCargo { get; set; }

        public int? IdPersonaEmpleado { get; set; }

        public string? Contraseña { get; set; }
        public virtual Cargo IdCargoNavigation { get; set; } = null!;

        public virtual Persona IdPersonaEmpleadoNavigation { get; set; } = null!;
    }
}
