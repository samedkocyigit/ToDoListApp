using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.DTOs;
using ToDoListApp.Models;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> AddUserAsync(UserRegisterDto user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);

}
