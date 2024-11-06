using DB.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Services
{
    public interface ICitaRepository
    {
        Task<Cita> CrearCitaAsync(Cita cita);
        Task<Cita> ObtenerCitaPorIdAsync(int idCita);
    }
}
