using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Peluqueria.DTO; 

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IPersonaService
    {
        Task<List<PersonaDTO>> Lista();
        Task<PersonaDTO> ObtenerPorId(int id);
        Task<PersonaDTO> Crear(PersonaDTO personaDto);
        Task<bool> Actualizar(PersonaDTO personaDto);
        Task<bool> Eliminar(int id);


    }
}
