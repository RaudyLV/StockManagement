using Application;
using Infraestructure.Identity.Seeds;
using Infraestructure.Persistence;
using Microsoft.OpenApi.Models;
using Presentation.Extensions;

public class Program
{

    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddAntiforgery();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddIdentityService(builder.Configuration);
        builder.Services.AddApplicationLayer();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using(var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await RoleSeeder.SeedRolesAndUserAsync(services); 
        }

        app.UseHttpsRedirection();
        app.ErrorHandlerMiddleware();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();

    }
}