using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peluqueria.DAL.DBContext;

using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DAL.Repositories;

using Peluqueria.Utility;
using System.Dynamic;
using AutoMapper;

namespace Peluqueria.IOC
{
    public static class Dependency
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
            });


            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IFacturaRepository, FacturaRepository>();

            services.AddSingleton(provider => new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }).CreateMapper());
        }

    }
}
