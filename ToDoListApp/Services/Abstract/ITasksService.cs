using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.DTOs;
using ToDoListApp.Models;

public interface ITasksService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync();
    Task<TaskDto> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId);
    Task<TaskDto> AddTaskAsync(CreateTaskDto task);
    Task<TaskDto> UpdateTaskAsync(UpdateTaskDto task);
    Task DeleteTaskAsync(int id);
    Task UpdateTaskStateAsync(int taskId, string newState);

}
