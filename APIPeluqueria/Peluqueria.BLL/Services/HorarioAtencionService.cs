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
    public class HorarioAtencionService : IHorarioAtencionService
    {
        private readonly IGenericRepository<HorarioAtencion> _horarioRepository;
        private readonly IMapper _mapper;

        public HorarioAtencionService(IGenericRepository<HorarioAtencion> horarioRepository, IMapper mapper)
        {
            _horarioRepository = horarioRepository;
            _mapper = mapper;
        }

        // Listar todos los horarios de atención
        public async Task<List<HorarioAtencionDTO>> ListarHorarios()
        {
            try
            {
                var horarios = await _horarioRepository.Consultar();
                return _mapper.Map<List<HorarioAtencionDTO>>(horarios.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener un horario específico por ID
        public async Task<HorarioAtencionDTO> ObtenerPorId(int id)
        {
            try
            {
                var horario = await _horarioRepository.Obtener(h => h.IdHorario == id);
                return _mapper.Map<HorarioAtencionDTO>(horario);
            }
            catch
            {
                throw;
            }
        }

        // Crear un nuevo horario de atención
        public async Task<HorarioAtencionDTO> Crear(HorarioAtencionDTO horarioAtencionDto)
        {
            try
            {
                var horario = _mapper.Map<HorarioAtencion>(horarioAtencionDto);
                var horarioCreado = await _horarioRepository.Crear(horario);
                return _mapper.Map<HorarioAtencionDTO>(horarioCreado);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar un horario existente
        public async Task<bool> Actualizar(HorarioAtencionDTO horarioAtencionDto)
        {
            try
            {
                var horarioExistente = await _horarioRepository.Obtener(h => h.IdHorario == horarioAtencionDto.IdHorario);
                if (horarioExistente == null)
                {
                    throw new KeyNotFoundException("Horario de atención no encontrado");
                }

                _mapper.Map(horarioAtencionDto, horarioExistente);
                return await _horarioRepository.Editar(horarioExistente);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar un horario de atención
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var horarioExistente = await _horarioRepository.Obtener(h => h.IdHorario == id);
                if (horarioExistente == null)
                {
                    throw new KeyNotFoundException("Horario de atención no encontrado");
                }

                return await _horarioRepository.Eliminar(horarioExistente);
            }
            catch
            {
                throw;
            }
        }
    }
}
