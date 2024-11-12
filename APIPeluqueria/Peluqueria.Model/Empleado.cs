namespace Peluqueria.Model
{
    public partial class Empleado : Persona
    {
        public double? Comision { get; set; } // Comisiones de los empleados
        public DateOnly? FechaContrato { get; set; }
        public int IdCargo { get; set; }

        // Relación con Cargo y HorarioAtencion
        public virtual Cargo? Cargo { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
