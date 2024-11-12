using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class CargoService : ICargoService
    {
        private readonly IGenericRepository<Cargo> _cargoRepository;
        private readonly IMapper _mapper;

        public CargoService(IGenericRepository<Cargo> cargoRepository, IMapper mapper)
        {
            _cargoRepository = cargoRepository;
            _mapper = mapper;
        }

        // Listar todos los cargos
        public async Task<List<CargoDTO>> Lista()
        {
            try
            {
                var listaCargo = await _cargoRepository.Consultar();
                return _mapper.Map<List<CargoDTO>>(listaCargo.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener un cargo por ID
        public async Task<CargoDTO> ObtenerPorId(int id)
        {
            try
            {
                var cargo = await _cargoRepository.Obtener(c => c.IdCargo == id);
                return _mapper.Map<CargoDTO>(cargo);
            }
            catch
            {
                throw;
            }
        }

        // Crear un nuevo cargo
        public async Task<CargoDTO> Crear(CargoDTO cargoDto)
        {
            try
            {
                var cargo = _mapper.Map<Cargo>(cargoDto);
                var cargoCreado = await _cargoRepository.Crear(cargo);
                return _mapper.Map<CargoDTO>(cargoCreado);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar un cargo existente
        public async Task<bool> Actualizar(CargoDTO cargoDto)
        {
            try
            {
                var cargoExistente = await _cargoRepository.Obtener(c => c.IdCargo == cargoDto.IdCargo);
                if (cargoExistente == null)
                {
                    throw new KeyNotFoundException("Cargo no encontrado");
                }

                _mapper.Map(cargoDto, cargoExistente);
                return await _cargoRepository.Editar(cargoExistente);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar un cargo
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var cargoExistente = await _cargoRepository.Obtener(c => c.IdCargo == id);
                if (cargoExistente == null)
                {
                    throw new KeyNotFoundException("Cargo no encontrado");
                }

                return await _cargoRepository.Eliminar(cargoExistente);
            }
            catch
            {
                throw;
            }
        }
    }
}
