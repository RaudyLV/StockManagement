
using Application.Enums;
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure.Identity.Seeds
{
    public static class DefaultRole
    {
        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}