using ProyectoO.DTO;
using System.Threading.Tasks;

namespace ProyectoO.Services.Interfaces
{
    
    public interface IPersonaService
    {
        string BaseUrl { get; }
        Task<PersonaDTO> GetPersonaByIdAsync(int idPersona);
        Task UpdatePersonaAsync(PersonaDTO persona);
        Task CrearPersonaAsync(PersonaDTO persona);
        Task EliminarPersonaAsync(int idPersona);
        Task<string> UploadProfilePictureAsync(int idPersona, string filePath);
    }

}
