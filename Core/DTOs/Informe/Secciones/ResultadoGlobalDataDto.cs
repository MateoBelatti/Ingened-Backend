using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class ResultadoGlobalDataDto
{
    [Required(ErrorMessage = "El campo Resultado Global es obligatorio.")]
    [RegularExpression("^(aceptable|aceptableConIndicaciones|noAceptable)$", 
        ErrorMessage = "El resultado global debe ser 'aceptable', 'aceptableConIndicaciones' o 'noAceptable'")]
    public string ResultadoGlobal { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Observaciones Generales es obligatorio.")]
    public string ObservacionesGenerales { get; set; } = string.Empty;
}
