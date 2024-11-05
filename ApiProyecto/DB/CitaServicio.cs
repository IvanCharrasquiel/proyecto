namespace DB
{
    public class CitaServicio
    {
        public int? IdCita { get; set; }

        public int? IdServicio { get; set; }

        public virtual Cita? IdCitaNavigation { get; set; }
    }
}
