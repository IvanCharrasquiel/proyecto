using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public class Empleadohorario
{
    public int IdEmpleado { get; set; }
    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public int IdHorario { get; set; }
    public virtual Horarioatencion IdHorarioNavigation { get; set; } = null!;

    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int DiaSemana { get; set; }
    public bool Disponible { get; set; }
}


