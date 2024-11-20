using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public int? Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? FotoPerfil { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
