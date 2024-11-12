using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class PromocionDTO
    {
        public int IdPromocion { get; set; }
        public string? Descripcion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public double? Descuento { get; set; }
        public string? Estado { get; set; }
        public int? IdServicio { get; set; }
        public ServicioDTO? Servicio { get; set; } // Navegación para mostrar detalles del servicio
    }
}
