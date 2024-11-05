namespace ApiProyecto.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public double? Comision { get; set; }

    public DateOnly? FechaContrato { get; set; }

    public int IdCargo { get; set; }

    public int IdPersonaEmpleado { get; set; }

    public string? Contraseña { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual Persona IdPersonaEmpleadoNavigation { get; set; } = null!;
}
