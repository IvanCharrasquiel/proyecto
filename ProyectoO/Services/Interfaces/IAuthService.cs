using ProyectoO.DTO;
using System.Threading.Tasks;

namespace ProyectoO.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(string email, string contraseña);
        Task RegisterAsync(RegistroRequestDTO registroRequest);
        void SetAuthorizationHeader(string token);
    }
}
