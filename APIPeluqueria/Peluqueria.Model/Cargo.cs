﻿namespace Peluqueria.Model;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string? Cargo1 { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
