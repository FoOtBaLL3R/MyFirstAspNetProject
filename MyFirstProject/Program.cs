using Microsoft.Extensions.DependencyInjection;
using MyFirstProject.DataAccess;

internal class Program
{
    private static async void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<MyNotesDbContext>();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<MyNotesDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

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