using System;

namespace Peluqueria.DTO
{
    public class SesionDTO
    {
        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? PersonaDescripcion { get; set; }

        // Nueva propiedad para indicar si es Cliente o Empleado
        public string TipoUsuario { get; set; } // "Cliente" o "Empleado"
    }
}
