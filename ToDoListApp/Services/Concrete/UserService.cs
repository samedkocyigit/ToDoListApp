using System.Collections.Generic;
using System.Threading.Tasks;
using static ToDoListApp.Data.Repositories.Abstract.IGenericRepository;
using ToDoListApp.Models;
using ToDoListApp.Data.Repositories.Abstract;
using ToDoListApp.DTOs;
using AutoMapper;

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

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
