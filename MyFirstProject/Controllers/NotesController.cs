using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Contracts;
using MyFirstProject.DataAccess;
using MyFirstProject.Models;
using System.Linq.Expressions;

namespace MyFirstProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly MyNotesDbContext _dbContext;

        public NotesController(MyNotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNoteRequest request, CancellationToken ct)
        {
            var note = new Note(request.Name, request.Description);

            await _dbContext.Notes.AddAsync(note, ct);
            await _dbContext.SaveChangesAsync();


            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetNotesRequest request, CancellationToken ct, int page = 1, int pageSize = 3)
        {
            var notesQuery = _dbContext.Notes
                .AsNoTracking()
                .Where(p => string.IsNullOrWhiteSpace(request.Search) ||
                p.Name.ToLower().Contains(request.Search.ToLower()));

            Expression<Func<Note, object>> selectorKey = request.SortItem?.ToLower() switch
            {
                "date" => note => note.CreatedAt,
                "name" => note => note.Name,
                _ => note => note.Id,
            };

            notesQuery = request.SortOrder == "desc" 
                ? notesQuery.OrderByDescending(selectorKey) 
                : notesQuery.OrderBy(selectorKey);

            var totalCount = notesQuery.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var noteDtos = await notesQuery
                .Select(p => new NoteDto(p.Id, p.Name, p.Description, p.CreatedAt))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken: ct);            

            return Ok(new GetNotesResponse(noteDtos, totalPages));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateNoteRequest request, CancellationToken ct)
        {
            await _dbContext.Notes
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.Name, n => request.Name)
                .SetProperty(n => n.Description, n => request.Description)
                );
            
            return Ok();

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dbContext.Notes
                .Where(n => n.Id == id)
                .ExecuteDeleteAsync();

            return Ok();

        }

    }
}
