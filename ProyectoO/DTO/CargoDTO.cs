using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoO.DTO
{
    public class CargoDTO
    {
        public int IdCargo { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCargo { get; set; } = null!;
    }
}
