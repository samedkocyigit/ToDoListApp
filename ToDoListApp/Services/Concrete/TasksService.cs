using System.Collections.Generic;
using System.Threading.Tasks;
using static ToDoListApp.Data.Repositories.Abstract.IGenericRepository;
using ToDoListApp.Models;
using ToDoListApp.Data.Repositories.Abstract;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public TasksService(ITasksRepository taskRepository , IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public async Task<Tasks> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }
    public async Task<IEnumerable<Tasks>> GetTasksByUserIdAsync(int id)
    {
        return await _taskRepository.GetTasksByUserIdAsync(id);
    }


    public async Task AddTaskAsync(Tasks task)
    {
        var user = await _userRepository.GetByIdAsync(task.UserId.Value);
        if(user != null)
        {
            user.Tasks.Add(task);
        } 
        await _taskRepository.AddAsync(task);
    }

    public async Task UpdateTaskAsync(Tasks task)
    {
        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteAsync(id);
    }
}
