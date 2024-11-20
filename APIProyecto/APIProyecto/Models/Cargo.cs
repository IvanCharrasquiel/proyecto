using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string NombreCargo { get; set; } = null!;
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

}
