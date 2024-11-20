namespace APIProyecto.DTO
{
    public class PromocionDTO
    {
        public int IdPromocion { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public decimal? Descuento { get; set; }
        public string? Estado { get; set; }
        public int IdServicio { get; set; }
    }
}
