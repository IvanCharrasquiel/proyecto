using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Cliente
    {

        public Cliente()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaRegistro { get; set; }

        public int? id_Persona { get; set; }

        public string? Contraseña { get; set; }
        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
    }



}
