using System;
using System.Collections.Generic;

namespace APIProyecto.Models
{
    public partial class Reserva
    {
        public int IdReserva { get; set; }

        public DateTime Fecha { get; set; } 

        public TimeSpan HoraInicio { get; set; } 

        public TimeSpan HoraFin { get; set; }

        public int IdCliente { get; set; }

        public int IdEmpleado { get; set; }

        public string EstadoReserva { get; set; } = "Confirmada";

        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        public virtual Cliente IdClienteNavigation { get; set; } = null!;

        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

        public virtual ICollection<Servicioreserva> Servicioreservas { get; set; } = new List<Servicioreserva>();
    }
}
