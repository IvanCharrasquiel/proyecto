using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class EmpleadoServicioService : IEmpleadoServicioService
    {
        private readonly IGenericRepository<EmpleadoServicio> _empleadoServicioRepository;
        private readonly IMapper _mapper;

        public EmpleadoServicioService(IGenericRepository<EmpleadoServicio> empleadoServicioRepository, IMapper mapper)
        {
            _empleadoServicioRepository = empleadoServicioRepository;
            _mapper = mapper;
        }

        // Listar todos los servicios que un empleado puede realizar
        public async Task<List<EmpleadoServicioDTO>> ListarServiciosPorEmpleado(int idEmpleado)
        {
            try
            {
                var servicios = await _empleadoServicioRepository.Consultar(es => es.IdEmpleado == idEmpleado);
                return _mapper.Map<List<EmpleadoServicioDTO>>(servicios.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Listar todos los empleados que pueden realizar un servicio específico
        public async Task<List<EmpleadoServicioDTO>> ListarEmpleadosPorServicio(int idServicio)
        {
            try
            {
                var empleados = await _empleadoServicioRepository.Consultar(es => es.IdServicio == idServicio);
                return _mapper.Map<List<EmpleadoServicioDTO>>(empleados.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Asignar un nuevo servicio a un empleado
        public async Task<EmpleadoServicioDTO> AsignarServicio(EmpleadoServicioDTO empleadoServicioDto)
        {
            try
            {
                var empleadoServicio = _mapper.Map<EmpleadoServicio>(empleadoServicioDto);
                var asignacionCreada = await _empleadoServicioRepository.Crear(empleadoServicio);
                return _mapper.Map<EmpleadoServicioDTO>(asignacionCreada);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar una asignación de servicio a un empleado
        public async Task<bool> EliminarAsignacion(int idEmpleado, int idServicio)
        {
            try
            {
                var asignacionExistente = await _empleadoServicioRepository.Obtener(es => es.IdEmpleado == idEmpleado && es.IdServicio == idServicio);
                if (asignacionExistente == null)
                {
                    throw new KeyNotFoundException("Asignación de servicio no encontrada");
                }

                return await _empleadoServicioRepository.Eliminar(asignacionExistente);
            }
            catch
            {
                throw;
            }
        }
    }
}
