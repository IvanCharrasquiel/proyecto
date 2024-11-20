// DTO/HorarioAtencionDTO.cs
using Newtonsoft.Json;
using System;

namespace ProyectoO.DTO
{
    public class HorarioAtencionDTO
    {
        [JsonProperty("IdHorario")]
        public int IdHorario { get; set; }

        [JsonProperty("HoraInicio")]
        public TimeSpan HoraInicio { get; set; }

        [JsonProperty("HoraFin")]
        public TimeSpan HoraFin { get; set; }
    }
}
