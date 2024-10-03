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
    }
}
