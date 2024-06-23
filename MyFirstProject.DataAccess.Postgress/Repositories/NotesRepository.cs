
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Core.Abstractions;
using MyFirstProject.Core.Models;
using MyFirstProject.DataAccess.Postgress.Entities;

namespace MyFirstProject.DataAccess.Postgress.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly MyNotesDbContext _context;
        public NotesRepository(MyNotesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> Get()
        {
            var notesEntity = await _context.Notes
                .AsNoTracking()
                .ToListAsync();

            var notes = notesEntity
                .Select(n => Note.Create(n.Id, n.Name, n.Description).Note)
                .ToList();
            return notes;
        }

        public async Task<Guid> Create(Note note)
        {
            var noteEntity = new NoteEntity
            {
                Id = note.Id,
                Name = note.Name,
                Description = note.Description,
                CreatedAt = note.CreatedAt,
            };

            await _context.Notes.AddAsync(noteEntity);
            await _context.SaveChangesAsync();

            return noteEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, DateTime createdAt)
        {
            await _context.Notes
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.Name, n => title)
                .SetProperty(n => n.Description, n => description)
                .SetProperty(n => n.CreatedAt, n => createdAt)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Notes
                .Where(n => n.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
