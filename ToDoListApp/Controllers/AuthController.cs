using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Domain.Services.AuthService;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var result = await _authService.Register(userRegisterDto);
        return Ok(result); 
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _authService.Login(userLoginDto);
        return Ok(new { User = user });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return NoContent();
    }
}

