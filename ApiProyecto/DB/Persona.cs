using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        [ForeignKey("IdSexo")]
        public int SexoID { get; set; }

        
    }
}
