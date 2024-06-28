namespace MyFirstProject.Contracts
{
    public record CreateUpdateNotesRequest(string Name, string Description, DateTime CreatedAt);
}
