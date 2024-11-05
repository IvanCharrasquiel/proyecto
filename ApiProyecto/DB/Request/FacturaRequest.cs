using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace DB.Request
{
    
    public class FacturaRequest
    {
        public FacturaRequest()
        {
            this.DetalleFacturas = new List<Detalles>();
            this.Pagos = new List<Pagos>();
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="El valor de la cita debe ser mayor a 0")]
        
        public int IdCita { get; set; }
        
        [Required]
        [MinLength(1,ErrorMessage ="Debe Existir Detalles")]

        public List<Detalles> DetalleFacturas { get; set; }
        public List<Pagos> Pagos { get; set; }


    }

    public class Detalles
    {
        public decimal Subtotal { get; set; }
        public int PrecioServicio { get; set; }
        public int IdServicio { get; set; }
        public int CantidadServicio { get; set; }

    }

    public class Pagos {
        public  decimal Monto { get; set; }
        public string Estado { get; set; }
        public  int IdFactura { get; set; }
        public int IdMetodoPago { get; set; }

    }



   
}
