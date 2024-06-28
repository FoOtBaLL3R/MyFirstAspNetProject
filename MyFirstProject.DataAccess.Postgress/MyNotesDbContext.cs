using Microsoft.EntityFrameworkCore;
using MyFirstProject.DataAccess.Postgress.Entities;

namespace MyFirstProject.DataAccess.Postgress
{
    public class MyNotesDbContext(DbContextOptions<MyNotesDbContext> options) : DbContext(options)
    {
        public DbSet<NoteEntity> Notes { get; set; }
    }
}
