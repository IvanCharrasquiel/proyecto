using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IEmpleadoServicioService
    {
        Task<List<EmpleadoServicioDTO>> ListarServiciosPorEmpleado(int idEmpleado);
        Task<List<EmpleadoServicioDTO>> ListarEmpleadosPorServicio(int idServicio);
        Task<EmpleadoServicioDTO> AsignarServicio(EmpleadoServicioDTO empleadoServicioDto);
        Task<bool> EliminarAsignacion(int idEmpleado, int idServicio);
    }
}
