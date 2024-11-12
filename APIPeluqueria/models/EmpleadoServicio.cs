namespace models;

public partial class EmpleadoServicio
{
    public int? IdEmpleado { get; set; }

    public int? IdServicio { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
