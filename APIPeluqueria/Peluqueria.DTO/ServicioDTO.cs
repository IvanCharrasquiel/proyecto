using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class ServicioDTO
    {
        public int IdServicio { get; set; }
        public string? NombreServicio { get; set; }
        public string? Descripcion { get; set; }
        public double? Precio { get; set; }
        public int? Duracion { get; set; }
    }
}
