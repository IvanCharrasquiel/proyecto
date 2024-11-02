using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServicio { get; set; }
        public string servicio { get; set; }
        public string Descripcion { get; set; }
        public float precio { get; set; }
        public int minutos { get; set; }


        [ForeignKey("IdCategoria")]
        public int IdCategoria { get; set; }
        
    }
}
