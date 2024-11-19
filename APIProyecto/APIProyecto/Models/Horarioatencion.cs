using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Horarioatencion
{
    public int IdHorario { get; set; }


    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public virtual ICollection<Empleadohorario> Empleadohorarios { get; set; } = new List<Empleadohorario>();
}
