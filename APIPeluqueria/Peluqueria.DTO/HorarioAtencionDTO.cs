using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class HorarioAtencionDTO
    {
        public int IdHorario { get; set; }
        public string? Dia { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
    }
}
