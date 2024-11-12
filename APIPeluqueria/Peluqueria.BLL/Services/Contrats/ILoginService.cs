using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface ILoginService
    {
        Task<string> ValidarCredenciales(LoginDTO loginDto); // Devuelve el token JWT
        Task CerrarSesion(int idUsuario); // Opcional, si necesitas cerrar sesión
    }
}
