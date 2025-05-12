using Infraestructure.Identity.Context;
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions 
{
    public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityDbConnection"),
            b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)
        ));

        services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
    }
}