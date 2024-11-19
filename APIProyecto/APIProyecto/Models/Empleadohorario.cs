using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Empleadohorario
{
    public int IdEmpleado { get; set; }

    public int IdHorario { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Horarioatencion IdHorarioNavigation { get; set; } = null!;
}
