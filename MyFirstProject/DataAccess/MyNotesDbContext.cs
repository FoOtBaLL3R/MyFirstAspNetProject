using Microsoft.EntityFrameworkCore;

namespace MyFirstProject.DataAccess
{
    public class MyNotesDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MyNotesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DataBase"));
        }
    }
}
