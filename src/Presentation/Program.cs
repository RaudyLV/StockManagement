using Application;
using Infraestructure.Identity.Seeds;
using Infraestructure.Persistence;
using Presentation.Extensions;

public class Program
{

    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddIdentityService(builder.Configuration);
        builder.Services.AddApplicationLayer();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
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

        app.MapControllers();

        app.Run();

    }
}