namespace APIProyecto.DTO
{
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? Estado { get; set; }
        public int IdFactura { get; set; }
        public int? IdMetodoPago { get; set; }

        // Puedes incluir información del método de pago si es necesario
        // public MetodoPagoDTO? MetodoPago { get; set; }
    }
}
