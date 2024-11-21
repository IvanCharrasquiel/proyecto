using System;
using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        public DateTime? FechaContrato { get; set; }

        public decimal Comision { get; set; }

        public int IdPersona { get; set; }

        public int IdCargo { get; set; }


        public PersonaDTO Persona { get; set; }
        public CargoDTO? Cargo { get; set; }

    }
}
