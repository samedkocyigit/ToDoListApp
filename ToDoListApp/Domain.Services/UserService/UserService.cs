using System.Collections.Generic;
using System.Threading.Tasks;
using static ToDoListApp.Infrastructure.Repositories.GenericRepository.IGenericRepository;
using AutoMapper;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Domain.Models.Models;
using ToDoListApp.Infrastructure.Repositories.UserRepository;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> AddUserAsync(UserRegisterDto userRegisterDto)
    {
        var user= _mapper.Map<User>(userRegisterDto);
        var newUser = await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(newUser);
    }

    public async Task<UserDto> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
