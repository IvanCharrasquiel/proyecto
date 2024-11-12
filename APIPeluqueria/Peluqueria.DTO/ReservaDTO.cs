using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }
        public DateOnly? Fecha { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEmpleado { get; set; }
        public string? EstadoReserva { get; set; }
        public EmpleadoDTO? Empleado { get; set; } // Navegación para mostrar detalles del empleado
        public List<ServicioDTO> Servicios { get; set; } = new List<ServicioDTO>(); // Navegación para mostrar servicios en la reserva
    }
}
