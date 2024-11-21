// Services/AuthService.cs
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProyectoO.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiService _apiService;

        public AuthService(string baseUrl)
        {
            _apiService = new ApiService(baseUrl);
        }

        public async Task<LoginResponseDTO> LoginAsync(string email, string contraseña)
        {
            var loginRequest = new LoginDTO { Email = email, Contraseña = contraseña };
            var response = await _apiService.PostAsync<LoginDTO, LoginResponseDTO>("api/Auth/Login", loginRequest);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                // Guardar el token en SecureStorage
                await SecureStorage.SetAsync("AuthToken", response.Token);

                // Establecer el token en el ApiService
                _apiService.SetAuthorizationHeader(response.Token);

                return response;
            }

            throw new Exception("Credenciales inválidas.");
        }


        public async Task RegisterAsync(RegistroRequestDTO registroRequest)
        {
            try
            {
                await _apiService.PostAsync<RegistroRequestDTO, object>("api/Cliente/Register", registroRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar: {ex.Message}");
            }
        }

        public void SetAuthorizationHeader(string token)
        {
            _apiService.SetAuthorizationHeader(token);
        }
    }
}
