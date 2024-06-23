namespace MyFirstProject.Contracts
{
    public record NotesRequest(
        string Name,
        string Description,
        DateTime CreatedAt);

}
