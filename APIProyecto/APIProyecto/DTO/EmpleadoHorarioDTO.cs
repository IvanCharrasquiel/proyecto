// DTO/EmpleadoHorarioDTO.cs
namespace APIProyecto.DTO
{
    public class EmpleadoHorarioDTO
    {
        public int IdEmpleado { get; set; }
        public int IdHorario { get; set; }
        public DateTime FechaInicio { get; set; } 
        public DateTime? FechaFin { get; set; }
        public int DiaSemana { get; set; }
        public bool Disponible { get; set; }
    }
}
