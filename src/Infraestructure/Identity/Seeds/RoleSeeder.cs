
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Identity.Seeds
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAndUserAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<AppUser>>();

            await DefaultRole.SeedRolesAsync(userManager ,roleManager);
        }
    }
}