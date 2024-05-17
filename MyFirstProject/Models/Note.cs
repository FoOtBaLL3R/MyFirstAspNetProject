namespace MyFirstProject.Models
{
    public class Note
    {
        public Note(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        
    }
}
