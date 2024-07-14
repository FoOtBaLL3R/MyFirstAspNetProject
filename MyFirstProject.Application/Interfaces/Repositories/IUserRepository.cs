using MyFirstProject.Core.Models;

namespace MyFirstProject.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}