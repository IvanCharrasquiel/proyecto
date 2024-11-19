namespace APIProyecto.DTO
{
    public class HorarioAtencionDTO
    {
        public int IdHorario { get; set; }
        public string Dia { get; set; } = null!;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
