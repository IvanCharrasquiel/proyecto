// ProyectoO/Services/ServicioService.cs
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoO.Services
{
    public class ServicioService : IServicioService
    {
        private readonly ApiService _apiService;

        public ServicioService(string baseUrl)
        {
            _apiService = new ApiService(baseUrl);
        }

        public async Task<List<ServicioDTO>> GetAllServiciosAsync()
        {
            string endpoint = "api/Servicios";
            try
            {
                var servicios = await _apiService.GetAsync<List<ServicioDTO>>(endpoint);
                return servicios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los servicios: {ex.Message}");
            }
        }

        public async Task<ServicioDTO> GetServicioByIdAsync(int idServicio)
        {
            string endpoint = $"api/Servicios/{idServicio}";
            try
            {
                var servicio = await _apiService.GetAsync<ServicioDTO>(endpoint);
                return servicio;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el servicio: {ex.Message}");
            }
        }

        public async Task ReservarReservaAsync(ReservaDTO reserva)
        {
            string endpoint = "api/Reservas"; // Asegúrate de que este endpoint es correcto
            try
            {
                await _apiService.PostAsync<ReservaDTO, object>(endpoint, reserva);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al reservar la reserva: {ex.Message}");
            }
        }

        // Otros métodos relacionados con servicios
    }
}
