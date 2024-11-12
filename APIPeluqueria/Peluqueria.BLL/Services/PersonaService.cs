using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class PersonaService : IPersonaService
    {

        private readonly IGenericRepository<Persona> _personaRepository;
        private readonly IMapper _mapper;

        public PersonaService(IGenericRepository<Persona> personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<List<PersonaDTO>> Lista()
        {
            try
            {
                var listaPersona = await _personaRepository.Consultar();
                return _mapper.Map<List<PersonaDTO>>(listaPersona.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<PersonaDTO> ObtenerPorId(int id)
        {
            try
            {
                var persona = await _personaRepository.Obtener(p => p.IdPersona == id);
                return _mapper.Map<PersonaDTO>(persona);
            }
            catch
            {
                throw;
            }
        }


        public async Task<PersonaDTO> Crear(PersonaDTO personaDto)
        {
            try
            {
                var persona = _mapper.Map<Persona>(personaDto);
                var personaCreada = await _personaRepository.Crear(persona);
                return _mapper.Map<PersonaDTO>(personaCreada);
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Actualizar(PersonaDTO personaDto)
        {
            try
            {
                var personaExistente = await _personaRepository.Obtener(p => p.IdPersona == personaDto.IdPersona);
                if (personaExistente == null)
                {
                    throw new KeyNotFoundException("Persona no encontrada");
                }

                _mapper.Map(personaDto, personaExistente);
                return await _personaRepository.Editar(personaExistente);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var personaExistente = await _personaRepository.Obtener(p => p.IdPersona == id);
                if (personaExistente == null)
                {
                    throw new KeyNotFoundException("Persona no encontrada");
                }

                return await _personaRepository.Eliminar(personaExistente);
            }
            catch
            {
                throw;
            }
        }

    }
}
