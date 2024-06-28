namespace MyFirstProject.Contracts
{
    public record NotesResponse(List<NoteDto> notes, int totalPages);

}
