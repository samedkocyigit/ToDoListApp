using ToDoListApp.Domain.Models.Models;
using static ToDoListApp.Infrastructure.Repositories.GenericRepository.IGenericRepository;

namespace ToDoListApp.Infrastructure.Repositories.TasksRepository
{
    public interface ITasksRepository : IGenericRepository<Tasks>
    {
        Task<IEnumerable<Tasks>> GetTasksByUserIdAsync(int id);
        Task UpdateTaskStateAsync(int taskId, string newState);

    }
}
