using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFirstProject.Application.Interfaces.Auth;
using MyFirstProject.Application.Interfaces.Repositories;
using MyFirstProject.Application.Services;
using MyFirstProject.DataAccess.Postgress;
using MyFirstProject.DataAccess.Postgress.Mappings;
using MyFirstProject.DataAccess.Postgress.Repositories;
using MyFirstProject.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        
        //services.AddAp(configuration);

        services.AddEndpointsApiExplorer();


        //services.AddTransient<ExceptionMiddleware>

        services.AddSwaggerGen();
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));


        //services.AddControllers();

        services.AddDbContext<MyNotesDbContext>(
            options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MyNotesDbContext)));
            }
            );

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        services.AddScoped<INotesService, NotesService>();
        services.AddScoped<INotesRepository, NotesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<NotesService>();
        services.AddScoped<UsersService>();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAutoMapper(typeof(DataBaseMappings));
        //services.AddAutoMapper(typeof(Data))

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseCors();

        //app.add
        //app.MapControllers();

        //app.AddMappedEndpoints();

        //app.MapGet("/", () => "Hello World!");]

        app.MapGet("get", () =>
        {
            return Results.Ok("ok");
        }).RequireAuthorization("AdminPolicy");

        app.Run();
    }

    
    
}