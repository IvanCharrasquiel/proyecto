using System;
using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class EmpleadoRegistroDTO
    {
        [Range(0, 100, ErrorMessage = "La comisión debe estar entre 0 y 100.")]
        public decimal? Comision { get; set; }
        public int IdCargo { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contraseña { get; set; } = null!;

        [Required]
        public PersonaDTO Persona { get; set; } = null!;
    }
}
