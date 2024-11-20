using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public class Horarioatencion
{
    public int IdHorario { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }

    // Propiedades de navegación
    public virtual ICollection<Empleadohorario> Empleadohorarios { get; set; } = new List<Empleadohorario>();
}
