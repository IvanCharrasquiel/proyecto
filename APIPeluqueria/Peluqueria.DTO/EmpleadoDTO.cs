namespace Peluqueria.DTO
{
    public class EmpleadoDTO : PersonaDTO
    {
        public double? Comision { get; set; }
        public DateOnly? FechaContrato { get; set; }
        public int IdCargo { get; set; }
        public CargoDTO? Cargo { get; set; } // Navegación para mostrar el cargo del empleado
    }
}
