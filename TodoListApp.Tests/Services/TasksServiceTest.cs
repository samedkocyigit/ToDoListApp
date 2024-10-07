using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Infrastructure.Repositories.TasksRepository;
using ToDoListApp.Infrastructure.Repositories.UserRepository;
using ToDoListApp.Domain.Models.Models;

namespace TodoListApp.Tests.Services;
public class TasksServiceTest
{
    private readonly Mock<ITasksRepository> _taskRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly IMapper _mapper;
    private readonly TasksService _tasksService;

    public TasksServiceTest()
    {
        _taskRepositoryMock = new Mock<ITasksRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Tasks, TaskDto>();
            cfg.CreateMap<CreateTaskDto, Tasks>();
            cfg.CreateMap<UpdateTaskDto, Tasks>();
        });

        _mapper = config.CreateMapper();

        _tasksService = new TasksService(_taskRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllTasksAsync_ShouldReturnTasksList()
    {
        var tasks = new List<Tasks>
        {
            new Tasks { Id = 1, Name = "Task 1" },
            new Tasks { Id = 2, Name = "Task 2" }
        };

        _taskRepositoryMock.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(tasks);

        var result = await _tasksService.GetAllTasksAsync();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<TaskDto>)result).Count);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
    {
        var task = new Tasks { Id = 1, Name = "Task 1" };

        _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(task);

        var result = await _tasksService.GetTaskByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task AddTaskAsync_ShouldReturnAddedTask()
    {
        var createTaskDto = new CreateTaskDto { Name = "New Task", UserId = 1 };
        var task = new Tasks { Id = 1, Name = "New Task", UserId = 1 };
        var user = new User { Id = 1, Username = "testuser", Tasks = new List<Tasks>() };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(user);

        _taskRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Tasks>()))
                           .ReturnsAsync(task);

        var result = await _tasksService.AddTaskAsync(createTaskDto);

        Assert.NotNull(result);
        Assert.Equal("New Task", result.Name);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldInvokeRepositoryDelete()
    {
        var taskId = 1;

        await _tasksService.DeleteTaskAsync(taskId);

        _taskRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
    }
}
