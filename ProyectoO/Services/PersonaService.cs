using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProyectoO.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly ApiService _apiService;

        public string BaseUrl => _apiService.BaseUrl;

        public PersonaService(string baseUrl)
        {
            _apiService = new ApiService(baseUrl);
        }

        public async Task<PersonaDTO> GetPersonaByIdAsync(int idPersona)
        {
            string endpoint = $"api/Personas/{idPersona}";
            

            try
            {
                // Asumiendo que el endpoint es GET /api/Persona/{id}
                var persona = await _apiService.GetAsync<PersonaDTO>($"api/Persona/{idPersona}");
                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la persona: {ex.Message}");
            }
        }

        public async Task UpdatePersonaAsync(PersonaDTO persona)
        {
            if (!persona.IdPersona.HasValue)
            {
                throw new ArgumentException("IdPersona no puede ser nulo.");
            }

            string endpoint = $"api/Personas/{persona.IdPersona.Value}";
            await _apiService.PutAsync<PersonaDTO, object>(endpoint, persona);
        }

        public async Task CrearPersonaAsync(PersonaDTO persona)
        {
            string endpoint = "api/Personas";
            await _apiService.PostAsync<PersonaDTO, object>(endpoint, persona);
        }

        public async Task EliminarPersonaAsync(int idPersona)
        {
            string endpoint = $"api/Personas/{idPersona}";
            await _apiService.DeleteAsync(endpoint);
        }

        public async Task<string> UploadProfilePictureAsync(int idPersona, string filePath)
        {
            string endpoint = $"api/Personas/{idPersona}/UploadProfilePicture";
            return await _apiService.UploadFileAsync(endpoint, filePath);
        }
    }
}