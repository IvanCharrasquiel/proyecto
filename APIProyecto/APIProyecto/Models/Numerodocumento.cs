using System;
using System.Collections.Generic;

namespace APIProyecto.Models;

public partial class Numerodocumento
{
    public int IdNumeroDocumento { get; set; }

    public int? UltimoNumero { get; set; }

    public DateOnly? FechaRegistro { get; set; }
}
