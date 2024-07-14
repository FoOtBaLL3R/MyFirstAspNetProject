
namespace MyFirstProject.DataAccess.Postgress.Entities
{
    public class UserEntity
    {        
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public List<NoteEntity> Notes { get; set; } = [];

        internal object Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
