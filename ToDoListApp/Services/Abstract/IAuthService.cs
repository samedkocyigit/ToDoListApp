using ToDoListApp.DTOs;

namespace ToDoListApp.Services.Abstract
{
    public interface IAuthService
    {
        Task<UserDto> Register(UserRegisterDto userRegisterDto);
        Task<LoginResponseDto> Login(UserLoginDto userLoginDto);
        Task Logout();
    }

}
