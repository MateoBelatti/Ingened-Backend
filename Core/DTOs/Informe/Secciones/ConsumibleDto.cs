using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs;

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

    // Array de archivos para recibir las imágenes directamente por form-data
    public IFormFile[]? Imagenes { get; set; }
}
