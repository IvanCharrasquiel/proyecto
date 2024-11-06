using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Services
{
    public interface IClienteRepository
    {
        Task<Cliente> ObtenerClientePorIdAsync(int idCliente);
    }
}
