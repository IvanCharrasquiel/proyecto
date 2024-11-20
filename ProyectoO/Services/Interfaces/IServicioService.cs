// ProyectoO/Services/Interfaces/IServicioService.cs
using ProyectoO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoO.Services.Interfaces
{
    public interface IServicioService
    {
        Task<List<ServicioDTO>> GetAllServiciosAsync();
        Task<ServicioDTO> GetServicioByIdAsync(int idServicio);
        Task ReservarReservaAsync(ReservaDTO reserva);
        // Otros métodos relacionados con servicios
    }
}
