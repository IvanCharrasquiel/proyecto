using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        
        
    }

    

}
