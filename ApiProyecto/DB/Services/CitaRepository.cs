using DB.Request;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Services
{
    public class CitaRepository : ICitaRepository
    {
        private readonly AppDbContext _context;
        public CitaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cita> CrearCitaAsync(Cita cita)
        {
            _context.Cita.Add(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task<Cita> ObtenerCitaPorIdAsync(int idCita)
        {
            return await _context.Cita.Include(c => c.IdClienteNavigation).Include(c => c.IdEmpleadoNavigation).Include(c => c.Id)
            .FirstOrDefaultAsync(c => c.IdCita == idCita);
        }
    }
}
