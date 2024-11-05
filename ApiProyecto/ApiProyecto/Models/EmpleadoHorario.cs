namespace ApiProyecto.Models;

public partial class EmpleadoHorario
{
    public int? IdEmpleado { get; set; }

    public int? IdHorario { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Horario? IdHorarioNavigation { get; set; }
}
