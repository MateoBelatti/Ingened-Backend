using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Common;

public class ConsumibleDto
{
    [Required(ErrorMessage = "El campo Producto es obligatorio.")]
    public string Producto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Lote es obligatorio.")]
    public string Lote { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Marca es obligatorio.")]
    public string Marca { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Vencimiento es obligatorio.")]
    public string Vencimiento { get; set; } = string.Empty;

    public IFormFile[]? Imagenes { get; set; }
}