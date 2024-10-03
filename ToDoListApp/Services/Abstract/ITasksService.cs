using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Models;

public interface ITasksService
{
    Task<IEnumerable<Tasks>> GetAllTasksAsync();
    Task<Tasks> GetTaskByIdAsync(int id);
    Task<IEnumerable<Tasks>> GetTasksByUserIdAsync(int userId);
    Task AddTaskAsync(Tasks task);
    Task UpdateTaskAsync(Tasks task);
    Task DeleteTaskAsync(int id);
}
