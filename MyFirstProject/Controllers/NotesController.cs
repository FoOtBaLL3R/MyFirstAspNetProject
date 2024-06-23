using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Contracts;
using MyFirstProject.Core.Abstractions;
using MyFirstProject.Core.Models;

namespace MyFirstProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotesResponse>>> GetNotes()
        {
            var notes = await _notesService.GetAllNotes();

            var response = notes.Select(n => new NotesResponse(n.Id, n.Name, n.Description, n.CreatedAt));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNote([FromBody] NotesRequest request)
        {
            var (note, error) = Note.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var noteId = await _notesService.CreateNote(note);

            return Ok(noteId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateNote(Guid id, [FromBody] NotesRequest request)
        {
            var noteId = await _notesService.UpdateNote(id, request.Name, request.Description, request.CreatedAt);

            return Ok(noteId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteNote(Guid id)
        {
            return Ok(await _notesService.DeleteNote(id));
        }
    }
}
