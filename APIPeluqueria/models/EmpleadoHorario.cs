namespace models;

public partial class EmpleadoHorario
{
    public int? IdEmpleado { get; set; }

    public int? IdHorario { get; set; }

    public DateOnly? Año { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual HorarioAtencion? IdHorarioNavigation { get; set; }
}
