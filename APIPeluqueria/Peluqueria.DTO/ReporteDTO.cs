﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class ReporteDTO
    {
        public string? NumeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public string? FechaRegistro { get; set; }

        public string? TotalFactura { get; set; }
        public string? Servicio { get; set; }
        public int? Cantidad { get; set; }
        public int? Precio { get; set; }
        public string? Total { get; set; }
    }
}
