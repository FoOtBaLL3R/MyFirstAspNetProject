
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Core.Abstractions;
using MyFirstProject.Core.Models;
using MyFirstProject.DataAccess.Postgress.Entities;
using System.Linq;
using System.Linq.Expressions;

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
            var notesEntity = _context.Notes
                .AsNoTracking();
            //.ToListAsync();

            var notes = notesEntity
                .Select(n => Note.Create(n.Id, n.Name, n.Description, n.CreatedAt).Note)
                .ToList();
            return notes;
        }

        public async Task<(List<Note>, int)> GetNotesBySearchFilterAndPage(string? search, string? sortOrder, string? sortItem, int page, int pageSize)
        {
            var notesEntity = _context.Notes
                .Where(p => string.IsNullOrWhiteSpace(search) ||
                p.Name.ToLower().Contains(search.ToLower()));

            Expression<Func<NoteEntity, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => note => note.CreatedAt,
                "name" => note => note.Name,
                _ => note => note.Id,
            };

            notesEntity = sortOrder == "desc"
                ? notesEntity.OrderByDescending(selectorKey)
                : notesEntity.OrderBy(selectorKey);

            var totalCount = notesEntity.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var notes = await notesEntity
                .Select(n => Note.Create(n.Id, n.Name, n.Description, n.CreatedAt).Note)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();





            //return Ok(new GetNotesResponse(noteDtos, totalPages));            

            return (notes, totalPages);
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
