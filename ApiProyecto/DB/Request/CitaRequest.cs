using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Request
{
    public class CitaRequest
    {
        public CitaRequest()
        {
            this.lcitaservicio = new List<citaservicios>();
            this.lfactura = new List<facturaCita>();
        }
        public int  IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdHorario { get; set; }

        public List<citaservicios> lcitaservicio { get; set; }
        public List<facturaCita> lfactura { get; set; }
    }

    public class facturaCita
    {
        public int IdCita { get; set; }
        public int MontoTotal { get; set; }
    }

    public class citaservicios
    {

        
    }

}
