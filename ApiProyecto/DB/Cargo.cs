using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Cargo
    {
       
        public Cargo()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCargo { get; set; }

        public int? Id { get; set; }

        [Column("Cargo")]
        [StringLength(50)]
        public string Cargo1 { get; set; }

        
    }
}
