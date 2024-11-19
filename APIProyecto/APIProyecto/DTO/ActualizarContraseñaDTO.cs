using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class ActualizarContraseñaDTO
    {
        [Required]
        [MinLength(6, ErrorMessage = "La contraseña actual debe tener al menos 6 caracteres.")]
        public string ContraseñaActual { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.")]
        public string NuevaContraseña { get; set; } = null!;
    }
}
