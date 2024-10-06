using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListApp.Data;
using ToDoListApp.Data.Repositories.Abstract;
using ToDoListApp.DTOs;
using ToDoListApp.Models;
using ToDoListApp.Services.Abstract;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<UserDto> Register(UserRegisterDto userRegisterDto)
    {
        var userExists = await _userRepository.GetUserByUsernameAsync(userRegisterDto.Username);
        if (userExists != null)
            throw new Exception("User already exists!");

        var user = new User
        {
            Username = userRegisterDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
            Email = userRegisterDto.Email
        };

        await _userRepository.AddAsync(user);

        return new UserDto
        {
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<LoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(userLoginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
        { 
            throw new Exception("Invalid credentials!");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new LoginResponseDto
        {
            email = user.Email,
            userId = user.Id,
            token = tokenHandler.WriteToken(token),
            name = user.Username
        };
    }

    public Task Logout()
    {
        return Task.CompletedTask;
    }
}
