using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data.Repositories.Abstract;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repositories.Concrete
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        private readonly ToDoAppContext _context;
        public UserRepository(ToDoAppContext context):base(context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
