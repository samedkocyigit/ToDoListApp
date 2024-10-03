﻿using Microsoft.AspNetCore.Mvc;
using ToDoListApp.DTOs;
using ToDoListApp.Services.Abstract;

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
        return Ok(result); // UserDto olarak geri döner
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = await _authService.Login(userLoginDto);
        return Ok(new { Token = token });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return NoContent();
    }
}

