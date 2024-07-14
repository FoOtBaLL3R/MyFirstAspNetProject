using System.Xml.Linq;

namespace MyFirstProject.Core.Models
{
    public class User
    {
        public const int MIN_PASS_LENGTH = 5;

        private User(Guid id, string userName, string passwordHash, string email)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
        }
        public Guid Id { get; set; }
        
        public string UserName { get; private set; }
        
        public string PasswordHash { get; private set; }
        
        public string Email { get; private set; }

        public static User Create(Guid id, string userName, string passwordHash, string email)
        {
            var error = string.Empty;
                        
            return new User(id, userName, passwordHash, email);
        }

    }
}
