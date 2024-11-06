using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public int? id_Persona { get; set; }

        public string? Contraseña { get; set; }

        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

        // Relación uno a uno con Persona (cada cliente tiene una persona asociada)
        public virtual Persona IdPersonaEmpleadoNavigation { get; set; } = null!;
    }



}
