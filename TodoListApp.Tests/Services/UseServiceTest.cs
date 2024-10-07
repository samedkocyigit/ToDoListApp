using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Infrastructure.Repositories.UserRepository;
using ToDoListApp.Domain.Models.Models;

namespace TodoListApp.Tests.Services;
public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UserServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<UserRegisterDto, User>();
        });

        _mapper = config.CreateMapper();

        _userService = new UserService(_userRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUsersList()
    {
        var users = new List<User>
        {
            new User { Id = 1, Username = "user1" },
            new User { Id = 2, Username = "user2" }
        };

        _userRepositoryMock.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(users);

        var result = await _userService.GetAllUsersAsync();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<UserDto>)result).Count);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        var user = new User { Id = 1, Username = "user1" };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(user);

        var result = await _userService.GetUserByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("user1", result.Username);
    }

    [Fact]
    public async Task AddUserAsync_ShouldReturnAddedUser()
    {
        var userRegisterDto = new UserRegisterDto { Username = "newUser" };
        var user = new User { Id = 1, Username = "newUser" };

        _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                           .ReturnsAsync(user);

        var result = await _userService.AddUserAsync(userRegisterDto);

        Assert.NotNull(result);
        Assert.Equal("newUser", result.Username);
    }

    [Fact]
    public async Task GetUserByUsernameAsync_ShouldReturnUser_WhenUserExists()
    {
        var user = new User { Id = 1, Username = "user1" };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync("user1"))
                           .ReturnsAsync(user);

        var result = await _userService.GetUserByUsernameAsync("user1");

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("user1", result.Username);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldInvokeRepositoryDelete()
    {
        var userId = 1;

        await _userService.DeleteUserAsync(userId);

        _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
    }
}
