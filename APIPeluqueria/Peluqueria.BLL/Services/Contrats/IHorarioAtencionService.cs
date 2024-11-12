using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IHorarioAtencionService
    {
        Task<List<HorarioAtencionDTO>> ListarHorarios();
        Task<HorarioAtencionDTO> ObtenerPorId(int id);
        Task<HorarioAtencionDTO> Crear(HorarioAtencionDTO horarioAtencionDto);
        Task<bool> Actualizar(HorarioAtencionDTO horarioAtencionDto);
        Task<bool> Eliminar(int id);
    }
}
