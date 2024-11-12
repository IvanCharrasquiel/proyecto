using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class EmpleadoHorarioService : IEmpleadoHorarioService
    {
        private readonly IGenericRepository<EmpleadoHorario> _empleadoHorarioRepository;
        private readonly IMapper _mapper;

        public EmpleadoHorarioService(IGenericRepository<EmpleadoHorario> empleadoHorarioRepository, IMapper mapper)
        {
            _empleadoHorarioRepository = empleadoHorarioRepository;
            _mapper = mapper;
        }

        // Listar todos los horarios de un empleado específico
        public async Task<List<EmpleadoHorarioDTO>> ListarPorEmpleado(int idEmpleado)
        {
            try
            {
                var horarios = await _empleadoHorarioRepository.Consultar(eh => eh.IdEmpleado == idEmpleado);
                return _mapper.Map<List<EmpleadoHorarioDTO>>(horarios.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener un horario específico de un empleado
        public async Task<EmpleadoHorarioDTO> ObtenerPorId(int idEmpleado, int idHorario)
        {
            try
            {
                var horario = await _empleadoHorarioRepository.Obtener(eh => eh.IdEmpleado == idEmpleado && eh.IdHorario == idHorario);
                return _mapper.Map<EmpleadoHorarioDTO>(horario);
            }
            catch
            {
                throw;
            }
        }

        // Crear un nuevo horario para un empleado
        public async Task<EmpleadoHorarioDTO> Crear(EmpleadoHorarioDTO empleadoHorarioDto)
        {
            try
            {
                var empleadoHorario = _mapper.Map<EmpleadoHorario>(empleadoHorarioDto);
                var horarioCreado = await _empleadoHorarioRepository.Crear(empleadoHorario);
                return _mapper.Map<EmpleadoHorarioDTO>(horarioCreado);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar un horario existente de un empleado
        public async Task<bool> Actualizar(EmpleadoHorarioDTO empleadoHorarioDto)
        {
            try
            {
                var horarioExistente = await _empleadoHorarioRepository.Obtener(eh => eh.IdEmpleado == empleadoHorarioDto.IdEmpleado && eh.IdHorario == empleadoHorarioDto.IdHorario);
                if (horarioExistente == null)
                {
                    throw new KeyNotFoundException("Horario no encontrado");
                }

                _mapper.Map(empleadoHorarioDto, horarioExistente);
                return await _empleadoHorarioRepository.Editar(horarioExistente);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar un horario de un empleado
        public async Task<bool> Eliminar(int idEmpleado, int idHorario)
        {
            try
            {
                var horarioExistente = await _empleadoHorarioRepository.Obtener(eh => eh.IdEmpleado == idEmpleado && eh.IdHorario == idHorario);
                if (horarioExistente == null)
                {
                    throw new KeyNotFoundException("Horario no encontrado");
                }

                return await _empleadoHorarioRepository.Eliminar(horarioExistente);
            }
            catch
            {
                throw;
            }
        }
    }
}
