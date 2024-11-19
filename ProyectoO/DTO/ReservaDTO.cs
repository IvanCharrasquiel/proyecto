using Newtonsoft.Json;

public class ReservaDTO
{
    [JsonProperty("IdReserva")]
    public int IdReserva { get; set; }

    [JsonProperty("Fecha")]
    public DateTime Fecha { get; set; }

    [JsonProperty("HoraInicio")]
    public TimeSpan HoraInicio { get; set; }

    [JsonProperty("HoraFin")]
    public TimeSpan HoraFin { get; set; }

    [JsonProperty("IdCliente")]
    public int IdCliente { get; set; }

    [JsonProperty("IdEmpleado")]
    public int IdEmpleado { get; set; }

    [JsonProperty("EstadoReserva")]
    public string EstadoReserva { get; set; }

    // Otros campos según tu base de datos
}