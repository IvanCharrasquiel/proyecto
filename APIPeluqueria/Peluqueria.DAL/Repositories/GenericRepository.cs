using Microsoft.EntityFrameworkCore;
using Peluqueria.DAL.Repositories.Contrats;
using System.Linq.Expressions;

namespace Peluqueria.DAL.Repositories
{
    public class GenericRepository<Tmodel> : IGenericRepository<Tmodel> where Tmodel : class
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tmodel> Obtener(Expression<Func<Tmodel, bool>> filtro)
        {
            try
            {
                Tmodel model = await _dbContext.Set<Tmodel>().FirstOrDefaultAsync(filtro);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tmodel> Crear(Tmodel model)
        {
            try
            {
                _dbContext.Set<Tmodel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Tmodel model)
        {
            try
            {
                _dbContext.Set<Tmodel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Tmodel model)
        {
            try
            {
                _dbContext.Set<Tmodel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<Tmodel>> Consultar(Expression<Func<Tmodel, bool>> filtro = null)
        {
            try
            {
                IQueryable<Tmodel> queryModel = filtro == null ? _dbContext.Set<Tmodel>() : _dbContext.Set<Tmodel>().Where(filtro);
                return queryModel;
            }
            catch
            {
                throw;
            }
        }




    }
}
