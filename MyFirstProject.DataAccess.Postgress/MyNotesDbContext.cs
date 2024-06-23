using Microsoft.EntityFrameworkCore;
using MyFirstProject.DataAccess.Postgress.Entities;

namespace MyFirstProject.DataAccess.Postgress
{
    public class MyNotesDbContext : DbContext
    {
        public MyNotesDbContext(DbContextOptions<MyNotesDbContext> options) 
            : base(options)
        {
        }

        public DbSet<NoteEntity> Notes { get; set; }
    }
}
