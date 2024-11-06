using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Request
{
    public class CitaRequest
    {
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdHorario {  get; set; }

        public ClienteRequest Cliente { get; set; }
        public EmpleadoRequest Empleado { get; set; }
        public ServicioRequest Servicio { get; set; }
        public FacturaRequest Factura { get; set; }

        public IEnumerable<citaservicio> Citas { get; set; } = Enumerable.Empty<citaservicio>();

    }

    

    public class citaservicio
    {
        [Key]
        public int IdCitaServicio { get; set; }
        public int IdCita { get; set; }
        public int IdServicio { get; set; }
    }

    

    

}
