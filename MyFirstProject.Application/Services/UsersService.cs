
using MyFirstProject.Application.Interfaces.Auth;
using MyFirstProject.Application.Interfaces.Repositories;
using MyFirstProject.Core.Models;

namespace MyFirstProject.Application.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;

        public UsersService(IJwtProvider jwtProvider, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task Register(string userName, string email, string password)
        {
            var passHashed = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, passHashed, email);

            await _userRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            var result = _passwordHasher.VerifyPassword(password, user.PasswordHash);

            if (result == false) 
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

    }
}
