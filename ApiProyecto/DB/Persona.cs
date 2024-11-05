using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Persona
    {

        public Persona()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersona { get; set; }

        public int? Cedula { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        public int? Telefono { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Direccion { get; set; }

        [StringLength(256)]

        public string ContraseñaPersona { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    }
}
