using Xunit;
using Moq;
using System.Threading.Tasks;
using ToDoListApp.Domain.Services.AuthService;
using ToDoListApp.Infrastructure.Repositories.UserRepository;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Domain.Models.Models;
using ToDoListApp.Exceptions;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Collections.Generic;

namespace TodoListApp.Tests.Services;
public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _configurationMock = new Mock<IConfiguration>();

        _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Register_ShouldThrowError_WhenUserAlreadyExists()
    {
        var userRegisterDto = new UserRegisterDto { Username = "existingUser", Password = "password", Email = "test@example.com" };
        var existingUser = new User { Username = "existingUser" };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userRegisterDto.Username))
                           .ReturnsAsync(existingUser);

        await Assert.ThrowsAsync<ErrorExceptions>(() => _authService.Register(userRegisterDto));
    }

    [Fact]
    public async Task Register_ShouldReturnUserDto_WhenUserIsRegisteredSuccessfully()
    {
        var userRegisterDto = new UserRegisterDto { Username = "newUser", Password = "password", Email = "test@example.com" };
        var newUser = new User { Username = "newUser", Email = "test@example.com", Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password) };
        var newUserDto = new UserDto { Username = "newUser", Email = "test@example.com" };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userRegisterDto.Username))
                           .ReturnsAsync((User)null);  
        _mapperMock.Setup(mapper => mapper.Map<User>(userRegisterDto)).Returns(newUser);
        _mapperMock.Setup(mapper => mapper.Map<UserDto>(newUser)).Returns(newUserDto);

        var result = await _authService.Register(userRegisterDto);

        _userRepositoryMock.Verify(repo => repo.AddAsync(newUser), Times.Once);
        Assert.Equal(newUserDto.Username, result.Username);
        Assert.Equal(newUserDto.Email, result.Email);
    }

    [Fact]
    public async Task Login_ShouldThrowError_WhenInvalidCredentialsProvided()
    {
        var userLoginDto = new UserLoginDto { Username = "nonExistingUser", Password = "wrongPassword" };
        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userLoginDto.Username))
                           .ReturnsAsync((User)null);  

        await Assert.ThrowsAsync<ErrorExceptions>(() => _authService.Login(userLoginDto));
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenValidCredentialsProvided()
    {
        var userLoginDto = new UserLoginDto { Username = "validUser", Password = "validPassword" };
        var user = new User { Username = "validUser", Password = BCrypt.Net.BCrypt.HashPassword(userLoginDto.Password), Email = "test@example.com", Id = 1 };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userLoginDto.Username))
                           .ReturnsAsync(user);
        _configurationMock.Setup(config => config["Jwt:Key"]).Returns("SUPER_ULTRA_MYSTERIOUS_SECRET_TOKEN_123456789");

        var result = await _authService.Login(userLoginDto);

        Assert.NotNull(result.token);
        Assert.Equal("test@example.com", result.email);
        Assert.Equal(1, result.userId);
        Assert.Equal("validUser", result.name);
    }
}
