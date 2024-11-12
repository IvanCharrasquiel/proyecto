using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> Lista();
        Task<EmpleadoDTO> ObtenerPorId(int id);
        Task<EmpleadoDTO> Crear(EmpleadoDTO empleadoDto);
        Task<bool> Actualizar(EmpleadoDTO empleadoDto);
        Task<bool> Eliminar(int id);
        Task<List<EmpleadoDTO>> EmpleadosPorCargo(int idCargo); // Listar empleados según el cargo (rol)
        Task<double?> ObtenerComision(int idEmpleado); // Obtener la comisión del empleado
    }
}
