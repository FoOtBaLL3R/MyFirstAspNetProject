namespace MyFirstProject.Contracts
{
    public record NotesResponse(
        Guid Id,
        string Name,
        string Description,
        DateTime CreatedAt);

}
