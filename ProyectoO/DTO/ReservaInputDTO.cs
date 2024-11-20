// DTOs/ReservaInputDTO.cs
using System;
using System.Collections.Generic;

namespace ProyectoO.DTO
{
    public class ReservaInputDTO
    {
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraInicio { get; set; } // Nullable para manejar el error
        public int IdEmpleado { get; set; }
        public List<int> Servicios { get; set; } = new List<int>();
        public string? EstadoReserva { get; set; }
    }
}
