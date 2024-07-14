using MyFirstProject.Core.Models;

namespace MyFirstProject.Application.Interfaces.Repositories
{
    public interface INotesService
    {
        Task<Guid> CreateNote(Note note);
        Task<Guid> DeleteNote(Guid id);
        Task<List<Note>> GetAllNotes();
        Task<(List<Note>, int)> GetNotesBySearchFilterAndPage(string? search, string? sortOrder, string? sortItem, int page, int pageSize);

        Task<Guid> UpdateNote(Guid id, string title, string description, DateTime dateTime);
    }
}