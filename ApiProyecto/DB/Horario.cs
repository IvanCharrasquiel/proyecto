using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Horario
    {
        public Horario()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHorario { get; set; }

        [StringLength(50)]
        public string Dia { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? HoraInicio { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? HoraFin { get; set; }



    }
}
