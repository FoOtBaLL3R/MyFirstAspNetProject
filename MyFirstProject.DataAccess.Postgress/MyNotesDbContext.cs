using Microsoft.EntityFrameworkCore;
using MyFirstProject.DataAccess.Postgress.Configurations;
using MyFirstProject.DataAccess.Postgress.Entities;

namespace MyFirstProject.DataAccess.Postgress
{
    public class MyNotesDbContext(DbContextOptions<MyNotesDbContext> options) : DbContext(options)
    {
        public DbSet<NoteEntity> Notes { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
