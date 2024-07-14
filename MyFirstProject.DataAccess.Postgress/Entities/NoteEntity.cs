
namespace MyFirstProject.DataAccess.Postgress.Entities
{
    public class NoteEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public UserEntity? User { get; set; }
    }
}
