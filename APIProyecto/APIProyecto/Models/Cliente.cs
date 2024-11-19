using System;
using System.Collections.Generic;

namespace APIProyecto.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int IdPersona { get; set; }
        public string Contraseña { get; set; } = null!;

        // Propiedades de navegación
        public virtual Persona IdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();  // Nueva colección de Facturas
    }
}
