using MyFirstProject.Core.Abstractions;
using MyFirstProject.Core.Models;

namespace MyFirstProject.Application.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<List<Note>> GetAllNotes()
        {
            return await _notesRepository.Get();
        }

        public async Task<Guid> CreateNote(Note note)
        {
            return await _notesRepository.Create(note);
        }

        public async Task<Guid> UpdateNote(Guid id, string title, string description, DateTime dateTime)
        {
            return await _notesRepository.Update(id, title, description, dateTime);
        }

        public async Task<Guid> DeleteNote(Guid id)
        {
            return await _notesRepository.Delete(id);
        }
    }
}
