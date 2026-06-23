using System.Security.Claims;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ingened.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InformeController : ControllerBase
{
    private readonly IInformeService _informeService;

    public InformeController(IInformeService informeService)
    {
        _informeService = informeService;
    }

    [HttpPost("generarInforme")]
    public async Task<IActionResult> GenerarInforme([FromForm] InformeDTO informeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            if (!int.TryParse(userIdStr, out int userId))
            {
                return Unauthorized(new { message = "Usuario no autorizado o token inválido." });
            }

            var nuevoInforme = await _informeService.GenerarYGuardarInformeAsync(informeDto, userId);

            return Ok(new 
            { 
                message = "Informe generado y guardado exitosamente.", 
                informe = nuevoInforme
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAllInformes()
    {
        var informes = await _informeService.GetAllInformesAsync();
        return Ok(informes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInformeById(int id)
    {
        var informe = await _informeService.GetInformeByIdAsync(id);
        if (informe == null)
        {
            return NotFound(new { message = $"Informe con ID {id} no encontrado." });
        }
        return Ok(informe);
    }
}
