using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IGenericRepository<Empleado> _empleadoRepository;
        private readonly IMapper _mapper;

        public EmpleadoService(IGenericRepository<Empleado> empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        // Listar todos los empleados
        public async Task<List<EmpleadoDTO>> Lista()
        {
            try
            {
                var listaEmpleado = await _empleadoRepository.Consultar();
                return _mapper.Map<List<EmpleadoDTO>>(listaEmpleado.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener un empleado por ID
        public async Task<EmpleadoDTO> ObtenerPorId(int id)
        {
            try
            {
                var empleado = await _empleadoRepository.Obtener(e => e.IdEmpleado == id);
                return _mapper.Map<EmpleadoDTO>(empleado);
            }
            catch
            {
                throw;
            }
        }

        // Crear un nuevo empleado
        public async Task<EmpleadoDTO> Crear(EmpleadoDTO empleadoDto)
        {
            try
            {
                var empleado = _mapper.Map<Empleado>(empleadoDto);
                var empleadoCreado = await _empleadoRepository.Crear(empleado);
                return _mapper.Map<EmpleadoDTO>(empleadoCreado);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar un empleado existente
        public async Task<bool> Actualizar(EmpleadoDTO empleadoDto)
        {
            try
            {
                var empleadoExistente = await _empleadoRepository.Obtener(e => e.IdEmpleado == empleadoDto.IdEmpleado);
                if (empleadoExistente == null)
                {
                    throw new KeyNotFoundException("Empleado no encontrado");
                }

                _mapper.Map(empleadoDto, empleadoExistente);
                return await _empleadoRepository.Editar(empleadoExistente);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar un empleado
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var empleadoExistente = await _empleadoRepository.Obtener(e => e.IdEmpleado == id);
                if (empleadoExistente == null)
                {
                    throw new KeyNotFoundException("Empleado no encontrado");
                }

                return await _empleadoRepository.Eliminar(empleadoExistente);
            }
            catch
            {
                throw;
            }
        }

        // Listar empleados por cargo (rol)
        public async Task<List<EmpleadoDTO>> EmpleadosPorCargo(int idCargo)
        {
            try
            {
                var empleadosPorCargo = await _empleadoRepository.Consultar(e => e.IdCargo == idCargo);
                return _mapper.Map<List<EmpleadoDTO>>(empleadosPorCargo.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener la comisión del empleado
        public async Task<double?> ObtenerComision(int idEmpleado)
        {
            try
            {
                var empleado = await _empleadoRepository.Obtener(e => e.IdEmpleado == idEmpleado);
                if (empleado == null)
                {
                    throw new KeyNotFoundException("Empleado no encontrado");
                }
                return empleado.Comision;
            }
            catch
            {
                throw;
            }
        }
    }
}
