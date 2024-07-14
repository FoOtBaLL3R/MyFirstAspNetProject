using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Application.Interfaces.Repositories;
using MyFirstProject.Contracts;
using MyFirstProject.Core.Models;
using MyFirstProject.DataAccess.Postgress.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
        public async Task<ActionResult<List<NotesResponse>>> GetNotes([FromQuery] NotesRequest notesRequest, int page = 1, int pageSize = 3)
        {
            //var notes = await _notesService.GetAllNotes();
            //var response = notes.Select(n => new NotesResponse(n.Id, n.Name, n.Description, n.CreatedAt));

            var (notes, totalPages) = await _notesService.GetNotesBySearchFilterAndPage(notesRequest.Search, notesRequest.SortOrder, notesRequest.SortItem, page, pageSize);
            
            var response = notes
                .Select(n => new NoteDto(n.Id, n.Name, n.Description, n.CreatedAt))
                .ToList();

            return Ok(new NotesResponse(response, totalPages));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNote([FromBody] CreateUpdateNotesRequest requestCreate)
        {
            var (note, error) = Note.Create(
                Guid.NewGuid(),
                requestCreate.Name,
                requestCreate.Description,
                requestCreate.CreatedAt
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var noteId = await _notesService.CreateNote(note);

            return Ok(noteId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateNote(Guid id, [FromBody] CreateUpdateNotesRequest requestUpdate)
        {
            var noteId = await _notesService.UpdateNote(id, requestUpdate.Name, requestUpdate.Description, requestUpdate.CreatedAt);

            return Ok(noteId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteNote(Guid id)
        {
            return Ok(await _notesService.DeleteNote(id));
        }
    }
}
