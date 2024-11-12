namespace Peluqueria.Model;

public partial class HorarioAtencion
{
    public int IdHorario { get; set; }

    public string? Dia { get; set; }

    public DateTime? HoraInicio { get; set; }

    public DateTime? HoraFin { get; set; }
}
