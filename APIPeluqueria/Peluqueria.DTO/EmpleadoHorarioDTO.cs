using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class EmpleadoHorarioDTO
    {
        public int? IdEmpleado { get; set; }
        public int? IdHorario { get; set; }
        public DateOnly? Año { get; set; }
    }
}
