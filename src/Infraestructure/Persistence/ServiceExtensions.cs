using Application.Interfaces;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DomainConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            
        }
    }
}