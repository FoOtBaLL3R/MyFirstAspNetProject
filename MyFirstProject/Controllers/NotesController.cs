using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Contracts;
using MyFirstProject.DataAccess;
using MyFirstProject.Models;

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
        public async Task<IActionResult> Get(GetNotesRequest request, CancellationToken ct)
        {
            var notesQuery = _dbContext.Notes
                .Where(p => !string.IsNullOrWhiteSpace(request.Search) &&
                p.Name.ToLower().Contains(request.Search.ToLower()));

            if(request.SortOrder == "desc")
            {
                notesQuery = notesQuery.OrderByDescending(p => p.CreatedAt);
            }
            else
            {
                notesQuery = notesQuery.OrderBy(p => p.CreatedAt);
            }

            var notes = await notesQuery.ToListAsync(ct);
        }
    }
}
