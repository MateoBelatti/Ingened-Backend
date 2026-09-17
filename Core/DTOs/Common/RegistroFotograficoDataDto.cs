using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Common;

public class RegistroFotograficoDataDto
{
    [Required(ErrorMessage = "Debe subir al menos una foto.")]
    public IFormFile[] Fotos { get; set; } = [];
}