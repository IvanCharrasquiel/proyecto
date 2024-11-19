using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }

        public int? Cedula { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; } = null!;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(200)]
        public string? Direccion { get; set; }

        public string? FotoPerfil { get; set; }
    }
}
