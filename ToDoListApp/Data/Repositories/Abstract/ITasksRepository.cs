using ToDoListApp.Models;
using static ToDoListApp.Data.Repositories.Abstract.IGenericRepository;

namespace ToDoListApp.Data.Repositories.Abstract
{
    public interface ITasksRepository:IGenericRepository<Tasks>
    {
        Task<IEnumerable<Tasks>> GetTasksByUserIdAsync(int id);
    }
}
