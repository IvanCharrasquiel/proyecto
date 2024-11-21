// DTO/DetalleFacturaDTO.cs
namespace ProyectoO.DTO
{
    public class DetalleFacturaDTO
    {
        public int IdDetalleFactura { get; set; }
        public decimal Subtotal { get; set; }
        public decimal PrecioServicio { get; set; }
        public int CantidadServicio { get; set; }
        public int IdFactura { get; set; }
        public int IdServicioReserva { get; set; }

        // Puedes agregar propiedades adicionales si lo deseas, como el nombre del servicio
        public string NombreServicio { get; set; } = string.Empty;
    }
}
