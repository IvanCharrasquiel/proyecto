using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersona { get; set; }

        public int Cedula { get; set; } 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; } 
        public string Email { get; set; }
        public string Direccion { get; set; }

        // Relación con Sexo
        public int IdSexo { get; set; }
        [ForeignKey("IdSexo")]
        public virtual Sexo Sexo { get; set; }
    }
}
