using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Domain.Models.Models;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> AddUserAsync(UserRegisterDto user);
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);


}
