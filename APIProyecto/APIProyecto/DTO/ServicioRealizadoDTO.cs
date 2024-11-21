
using System;

namespace APIProyecto.DTO
{
    public class ServicioRealizadoDTO
    {
        public DateTime Fecha { get; set; }
        public string NombreServicio { get; set; }
        public decimal Precio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Empleado { get; set; }
    }
}
