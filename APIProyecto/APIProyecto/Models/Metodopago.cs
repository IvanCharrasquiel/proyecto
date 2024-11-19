using System.Collections.Generic;

namespace APIProyecto.Models
{
    public partial class Metodopago
    {
        public int IdMetodoPago { get; set; }
        public string Nombre { get; set; } = null!;

        // Nueva propiedad de navegación
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
