using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Wrappers;
using Core.Domain.Settings;
using Infraestructure.Identity.Context;
using Infraestructure.Identity.Helpers;
using Infraestructure.Identity.Models;
using Infraestructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<JWTHelper>();

        var key = configuration["JWTSettings:Key"]
                                    ?? throw new ArgumentNullException("La 'Key' no fue encontrada en JWTSettings:Key");

        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = 
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme = 
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(op =>
        {
            op.RequireHttpsMetadata = false;
            op.SaveToken = false;
            op.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidAudience = configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                RoleClaimType = ClaimTypes.Role,
            };

            op.Events = new JwtBearerEvents()
            {

             OnAuthenticationFailed = async context =>
            {            
                context.NoResult();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new Response<string>("Error de autenticación"));
                await context.Response.WriteAsync(result);

            },

            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new Response<string>("No estás autenticado."));
                await context.Response.WriteAsync(result);
            },

            OnForbidden = async context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new Response<string>("No tienes permisos para esta sección."));
                Console.WriteLine(context.Response.StatusCode.ToString());
                await context.Response.WriteAsync(result);
            }
            };
        });
    }
}