namespace APIProyecto.DTO
{
    public class RegisterDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string FotoPerfil { get; set; }
        public DateTime? FechaRegistro { get; set; } // Solo para Clientes
        public DateTime? FechaContrato { get; set; } // Solo para Empleados
        public decimal? Comision { get; set; } // Solo para Empleados
        public int? IdCargo { get; set; } // Solo para Empleados
        public string Contraseña { get; set; }
        public string Rol { get; set; } // "Cliente" o "Empleado"
    }
}
