using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }
    public int Duracion { get; set; }
    public virtual ICollection<Promocion> Promocions { get; set; } = new List<Promocion>();

    public virtual ICollection<Servicioreserva> Servicioreservas { get; set; } = new List<Servicioreserva>();

    public virtual ICollection<Empleado> IdEmpleados { get; set; } = new List<Empleado>();
}
