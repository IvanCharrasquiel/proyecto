namespace APIProyecto.DTO
{
    public class ReservaInputDTO
    {
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public int IdEmpleado { get; set; }
        public string? EstadoReserva { get; set; }
        public List<int> Servicios { get; set; } = new List<int>();
    }
}