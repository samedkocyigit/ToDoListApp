using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Domain.Models.Models;
using ToDoListApp.Exceptions;
using ToDoListApp.Infrastructure.Repositories.GenericRepository;

namespace ToDoListApp.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ToDoAppContext _context;
        public UserRepository(ToDoAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }
    }
}
