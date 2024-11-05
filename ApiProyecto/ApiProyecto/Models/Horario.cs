namespace ApiProyecto.Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public string? Dia { get; set; }

    public DateTime? HoraInicio { get; set; }

    public DateTime? HoraFin { get; set; }
}
