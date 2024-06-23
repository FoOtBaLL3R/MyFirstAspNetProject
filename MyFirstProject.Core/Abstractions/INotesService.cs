using MyFirstProject.Core.Models;

namespace MyFirstProject.Core.Abstractions
{
    public interface INotesService
    {
        Task<Guid> CreateNote(Note note);
        Task<Guid> DeleteNote(Guid id);
        Task<List<Note>> GetAllNotes();
        Task<Guid> UpdateNote(Guid id, string title, string description, DateTime dateTime);
    }
}