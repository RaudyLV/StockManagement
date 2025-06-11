using Application.Interfaces;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infraestructure.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DomainConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


            services.AddSingleton<IConnectionMultiplexer>(sp =>
                    ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));


            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<ICacheService, CacheService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IInventoryMovementService, InventoryMovementService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}