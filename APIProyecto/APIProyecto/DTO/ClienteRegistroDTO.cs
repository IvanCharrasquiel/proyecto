using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class ClienteRegistroDTO
    {
        [Required]
        [MinLength(6)]
        public string Contraseña { get; set; } = null!;
        

        [Required]
        public PersonaDTO Persona { get; set; } = null!;
    }
}
