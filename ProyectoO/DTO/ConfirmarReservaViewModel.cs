namespace ProyectoO.DTO
{
    public class ConfirmarReservaViewModel
    {
        public ServicioDTO Servicio { get; set; }
        public EmpleadoDTO Empleado { get; set; }
        public DateTime Fecha { get; set; }
        public HorarioAtencionDTO Horario { get; set; }
        public string PromocionesAplicables { get; set; }
        public bool TienePromociones { get; set; }
    }
}