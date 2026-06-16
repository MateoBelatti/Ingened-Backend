using Api.DTOs.Auth;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ingened.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

        if (token == null)
            return Unauthorized(new { message = "Credenciales incorrectas" });

        return Ok(new { Token = token });
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _authService.GoogleLoginAsync(googleLoginDto.IdToken);

        if (token == null)
            return Unauthorized(new { message = "Token de Google inválido" });

        return Ok(new { Token = token });
    }
}
