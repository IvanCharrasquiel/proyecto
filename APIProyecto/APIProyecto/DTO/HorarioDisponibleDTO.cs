namespace APIProyecto.DTO
{
    public class HorarioDisponibleDTO
    {
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Disponible { get; set; }
    }
}
