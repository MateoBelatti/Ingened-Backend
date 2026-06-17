using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ingened.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Todas las rutas requieren token JWT por defecto
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        
        // Retornamos un objeto anónimo omitiendo la contraseña cifrada
        var usersResponse = users.Select(u => new 
        { 
            u.Id, 
            u.Nombre, 
            u.Email, 
            u.UltimaConeccion, 
            u.GoogleId 
        });

        return Ok(usersResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        
        if (user == null)
            return NotFound();

        return Ok(new { user.Id, user.Nombre, user.Email, user.UltimaConeccion, user.GoogleId });
    }

    [HttpPost]
    [AllowAnonymous] // Permitimos crear un usuario sin token para que puedas probar el primer registro desde Postman
    public async Task<IActionResult> Create([FromBody] UserCreateDTO createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userService.GetByEmailAsync(createDto.Email);
        if (existingUser != null)
            return BadRequest(new { message = "El email ya está en uso" });

        var user = new User
        {
            Nombre = createDto.Nombre,
            Email = createDto.Email,
            Password = createDto.Password
        };

        var createdUser = await _userService.AddAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, new 
        { 
            createdUser.Id, 
            createdUser.Nombre, 
            createdUser.Email 
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        if (!string.IsNullOrEmpty(updateDto.Nombre))
            user.Nombre = updateDto.Nombre;
            
        if (!string.IsNullOrEmpty(updateDto.Email))
            user.Email = updateDto.Email;

        await _userService.UpdateAsync(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
