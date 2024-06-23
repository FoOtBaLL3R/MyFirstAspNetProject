using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFirstProject.Application.Services;
using MyFirstProject.Core.Abstractions;
using MyFirstProject.DataAccess.Postgress;
using MyFirstProject.DataAccess.Postgress.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddDbContext<MyNotesDbContext>(
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

        builder.Services.AddScoped<INotesService, NotesService>();
        builder.Services.AddScoped<INotesRepository, NotesRepository>();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.MapControllers();

        //app.MapGet("/", () => "Hello World!");

        app.Run();
    }

    
    
}