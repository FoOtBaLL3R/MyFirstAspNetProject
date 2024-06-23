namespace MyFirstProject.Core.Models
{
    public class Note
    {
        public const int MAX_NAME_LENGTH = 20;
        
        public const int MAX_DESC_LENGTH = 256;
        
        private Note(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public DateTime CreatedAt { get; init; }

        public static (Note Note, string Error) Create(Guid id, string name, string description)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH) 
            {
                error = "Name of note can't be empty or longer than 20 symbols";
            }
            if (string.IsNullOrEmpty(description) || description.Length > MAX_DESC_LENGTH)
            {
                error = string.IsNullOrEmpty(error) ? "Description of note can't be empty or longer than 20 symbols" : 
                    error + "\nDescription of note can't be empty or longer than 20 symbols";
            }

            var note = new Note(id, name, description);

            return (note, error);
        }
    }
}
