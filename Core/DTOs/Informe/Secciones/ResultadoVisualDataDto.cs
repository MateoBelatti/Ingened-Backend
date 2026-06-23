using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class ResultadoVisualDataDto
{
    [Required(ErrorMessage = "El campo Resultado Visual es obligatorio.")]
    [RegularExpression("^(aceptable|aceptableConIndicaciones|noAceptable)$", 
        ErrorMessage = "El resultado visual debe ser 'aceptable', 'aceptableConIndicaciones' o 'noAceptable'")]
    public string ResultadoVisual { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Observaciones es obligatorio.")]
    public string Observaciones { get; set; } = string.Empty;
}
