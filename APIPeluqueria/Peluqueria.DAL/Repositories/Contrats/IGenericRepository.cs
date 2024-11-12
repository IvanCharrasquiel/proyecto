using System.Linq.Expressions;

namespace Peluqueria.DAL.Repositories.Contrats
{
    public interface IGenericRepository<Tmodel> where Tmodel : class
    {
        Task<Tmodel> Obtener(Expression<Func<Tmodel, bool>> filtro);
        Task<Tmodel> Crear(Tmodel model);
        Task<bool> Editar(Tmodel model);
        Task<bool> Eliminar(Tmodel model);
        Task<IQueryable<Tmodel>> Consultar(Expression<Func<Tmodel, bool>> filtro = null);

    }
}
