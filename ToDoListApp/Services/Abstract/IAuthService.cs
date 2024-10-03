using ToDoListApp.DTOs;

namespace ToDoListApp.Services.Abstract
{
    public interface IAuthService
    {
        Task<UserDto> Register(UserRegisterDto userRegisterDto);
        Task<string> Login(UserLoginDto userLoginDto);
        Task Logout();
    }

}
