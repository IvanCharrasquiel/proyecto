using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersona { get; set; }  // Llave primaria

        [Range(1000000, 99999999)]  // Validación para cédula, ajusta el rango según tu país
        public int? Cedula { get; set; }

        [Required]  // Requerido
        [StringLength(50)]  // Limitar la longitud
        public string Nombre { get; set; }

        [Required]  // Requerido
        [StringLength(50)]  // Limitar la longitud
        public string Apellido { get; set; }

        [Range(1000000000, 9999999999)]  // Validación para teléfono
        public int? Telefono { get; set; }

        [Required]  // Requerido
        [EmailAddress]  // Validación de formato de email
        [StringLength(100)]  // Limitar la longitud
        public string Email { get; set; }

        [StringLength(200)]  // Limitar la longitud
        public string Direccion { get; set; }

        [Required]  // Requerido
        [StringLength(50)]  // Limitar la longitud
        public string ContraseñaPersona { get; set; }

        // Relación con Empleado (si es aplicable)
        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

        // Relación con Cliente (si es aplicable)
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }


}
