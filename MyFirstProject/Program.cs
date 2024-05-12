using Microsoft.Extensions.DependencyInjection;
using MyFirstProject.DataAccess;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<MyNotesDbContext>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}