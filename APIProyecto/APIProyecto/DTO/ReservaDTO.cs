// DTO/ReservaDTO.cs
using System;
using System.Collections.Generic;

namespace APIProyecto.DTO
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public string EstadoReserva { get; set; } = "Pendiente";
        public List<int> IdServicios { get; set; } = new List<int>();
    }
}
