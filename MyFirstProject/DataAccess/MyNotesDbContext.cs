using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.DataAccess
{
    public class MyNotesDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MyNotesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Note> Notes => Set<Note>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DataBase"));
        }
    }
}
