using Core.Interfaces;
using Core.DTOs;
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

        var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

        if (result == null)
            return Unauthorized(new { message = "Credenciales incorrectas" });

        return Ok(result);
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.GoogleLoginAsync(googleLoginDto.IdToken);

        if (result == null)
            return Unauthorized(new { message = "Token de Google inválido" });

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO refreshDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RefreshTokenAsync(refreshDto.RefreshToken);

        if (result == null)
            return Unauthorized(new { message = "Token de refresco inválido o expirado" });

        return Ok(result);
    }
}
