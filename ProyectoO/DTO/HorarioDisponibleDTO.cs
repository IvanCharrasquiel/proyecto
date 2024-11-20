// DTOs/HorarioDisponibleDTO.cs
using System;

namespace ProyectoO.DTO
{
    public class HorarioDisponibleDTO
    {
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Disponible { get; set; }
        public bool IsSelected { get; set; } // Para manejar la selección en la UI
    }
}
