namespace APIProyecto.DTO
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public DateTime? FechaEmision { get; set; }
        public decimal? MontoTotal { get; set; }
        public string? Estado { get; set; }
        public int IdReserva { get; set; }
        public int? IdCliente { get; set; }

        // Puedes incluir información del cliente si es necesario
        // public ClienteDTO? Cliente { get; set; }
    }
}
