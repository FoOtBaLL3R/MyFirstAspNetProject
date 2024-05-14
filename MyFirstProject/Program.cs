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

        using var scope = app.Services.CreateScope();
        Task task = MyAsyncMethod(scope);
        task.Wait();
        

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }

    
    private static async Task MyAsyncMethod(IServiceScope scpe)
    {
        await using var dbContext = scpe.ServiceProvider.GetRequiredService<MyNotesDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }
}