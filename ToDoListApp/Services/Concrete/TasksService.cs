using System.Collections.Generic;
using System.Threading.Tasks;
using static ToDoListApp.Data.Repositories.Abstract.IGenericRepository;
using ToDoListApp.Models;
using ToDoListApp.Data.Repositories.Abstract;
using ToDoListApp.DTOs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public TasksService(ITasksRepository taskRepository , IUserRepository userRepository,IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
    {
       var tasks=  await _taskRepository.GetAllAsync();
       return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetTaskByIdAsync(int id)
    {
        var  task = await _taskRepository.GetByIdAsync(id);
        return _mapper.Map<TaskDto>(task);
    }
    public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int id)
    {
        
        var tasks = await _taskRepository.GetTasksByUserIdAsync(id);
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }


    public async Task<TaskDto> AddTaskAsync(CreateTaskDto createTaskDto)
    {
        var task = _mapper.Map<Tasks>(createTaskDto);
        task.CreatedAt = DateTime.UtcNow;
        var user = await _userRepository.GetByIdAsync(task.UserId.Value);
        if(user != null)
        {
            user.Tasks.Add(task);
        } 
       
        var addedTask =await _taskRepository.AddAsync(task);
        return _mapper.Map<TaskDto>(addedTask);
    }

    public async Task<TaskDto> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
    {
        var task = _mapper.Map<Tasks>(updateTaskDto);

        var existingTask = await _taskRepository.GetByIdAsync(task.Id);
        if (existingTask == null)
        {
            throw new KeyNotFoundException("Task not found.");
        }

        existingTask.Name = updateTaskDto.Name ?? existingTask.Name;
        existingTask.Description = updateTaskDto.Description ?? existingTask.Description;
        
        if (updateTaskDto.State.HasValue) 
        {
            existingTask.State = updateTaskDto.State.Value;
        }

        var updatedTask = await _taskRepository.UpdateAsync(existingTask);
        return _mapper.Map<TaskDto>(updatedTask);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteAsync(id);
    }
    public async Task UpdateTaskStateAsync(int taskId, string newState)
    {
        await _taskRepository.UpdateTaskStateAsync(taskId, newState);
    }
}
