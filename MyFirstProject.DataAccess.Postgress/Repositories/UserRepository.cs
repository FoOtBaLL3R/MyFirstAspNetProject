using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Application.Interfaces.Repositories;
using MyFirstProject.Core.Models;
using MyFirstProject.DataAccess.Postgress.Entities;


namespace MyFirstProject.DataAccess.Postgress.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyNotesDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(MyNotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            /* Id { get; set; }
             UserName PasswordHash Email*/

            //var user = userEntity
            //    .Select(u => User.Create(u.Id, n.UserName, n.PasswordHash, n.Email).User);

            //return user;

            return _mapper.Map<User>(userEntity);
        }
    }
}
