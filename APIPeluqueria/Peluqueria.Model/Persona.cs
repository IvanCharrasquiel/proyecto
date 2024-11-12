namespace Peluqueria.Model;

public partial class Persona
{
    public int IdPersona { get; set; }

    public int? Cedula { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
