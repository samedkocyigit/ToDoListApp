using ToDoListApp.Domain.Models.Models;
using static ToDoListApp.Infrastructure.Repositories.GenericRepository.IGenericRepository;

namespace ToDoListApp.Infrastructure.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);

    }
}
