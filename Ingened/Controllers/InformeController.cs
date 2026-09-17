using System.Security.Claims;
using Core.DTOs.InformeLp;
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

    [HttpPost("lp")]
    public async Task<IActionResult> GenerarLp([FromForm] InformeLpDto dto)
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

            var nuevoInforme = await _informeService.GenerarLpAsync(dto, userId);

            return Ok(new
            {
                message = "Informe LP generado y guardado exitosamente.",
                informe = nuevoInforme
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (NotSupportedException ex)
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