namespace DB.Request
{
    public class ClienteRequest
    {
        public int IdCliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? id_Persona { get; set; }
        public string? Contraseña { get; set; }

    }
}
