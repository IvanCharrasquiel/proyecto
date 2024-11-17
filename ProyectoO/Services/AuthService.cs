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
            var loginData = new LoginDTO
            {
                Email = email.Trim(),
                Contraseña = contraseña.Trim()
            };

            try
            {
                var response = await _apiService.PostAsync<LoginDTO, LoginResponseDTO>("api/Auth/Login", loginData);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al iniciar sesión: {ex.Message}");
            }
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
