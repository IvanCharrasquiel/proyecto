using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class RecuperarContraseñaDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
