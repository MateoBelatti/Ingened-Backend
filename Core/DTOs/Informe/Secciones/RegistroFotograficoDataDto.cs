using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class RegistroFotograficoDataDto
{
    // Al usar IFormFile, ya no necesitas una clase "Foto" separada,
    // porque el objeto IFormFile ya incluye el nombre del archivo (.FileName)
    // y el contenido (stream), reemplazando el nombre y el base64.
    
    [Required(ErrorMessage = "Debe subir al menos una foto.")]
    public IFormFile[] Fotos { get; set; } = [];
}
