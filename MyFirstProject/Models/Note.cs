namespace MyFirstProject.Models
{
    public class Note
    {
        public Note(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public DateTime CreatedAt { get; init; }
        
    }
}
