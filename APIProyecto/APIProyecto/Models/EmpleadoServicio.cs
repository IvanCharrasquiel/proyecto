namespace APIProyecto.Models
{
    public class EmpleadoServicio
    {
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; } = null!;

        public int IdServicio { get; set; }
        public virtual Servicio Servicio { get; set; } = null!;
    }



}
