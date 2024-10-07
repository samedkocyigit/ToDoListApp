using ToDoListApp.Domain.Models.DTOs;

namespace ToDoListApp.Domain.Services.AuthService
{
    public interface IAuthService
    {
        Task<UserDto> Register(UserRegisterDto userRegisterDto);
        Task<LoginResponseDto> Login(UserLoginDto userLoginDto);
        Task Logout();
    }

}
