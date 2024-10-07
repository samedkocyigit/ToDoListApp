using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListApp.Infrastructure;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Domain.Services.AuthService;
using ToDoListApp.Infrastructure.Repositories.UserRepository;
using ToDoListApp.Domain.Models.Models;
using ToDoListApp.Exceptions;
using AutoMapper;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;


    public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<UserDto> Register(UserRegisterDto userRegisterDto)
    {
        var userExists = await _userRepository.GetUserByUsernameAsync(userRegisterDto.Username);
        if (userExists != null)
            throw new ErrorExceptions("User already exists!");

        var user = _mapper.Map<User>(userRegisterDto);
        user.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);



        await _userRepository.AddAsync(user);

        return _mapper.Map<UserDto>(user);

    }

    public async Task<LoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(userLoginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
        { 
            throw new ErrorExceptions("Invalid credentials!");
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
