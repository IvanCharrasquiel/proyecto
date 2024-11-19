namespace APIProyecto.DTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = null!;
        public EmpleadoDTO? Empleado { get; set; }
        public ClienteDTO? Cliente { get; set; }
    }
}
