using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IEmpleadoHorarioService
    {
        Task<List<EmpleadoHorarioDTO>> ListarPorEmpleado(int idEmpleado);
        Task<EmpleadoHorarioDTO> ObtenerPorId(int idEmpleado, int idHorario);
        Task<EmpleadoHorarioDTO> Crear(EmpleadoHorarioDTO empleadoHorarioDto);
        Task<bool> Actualizar(EmpleadoHorarioDTO empleadoHorarioDto);
        Task<bool> Eliminar(int idEmpleado, int idHorario);
    }
}
