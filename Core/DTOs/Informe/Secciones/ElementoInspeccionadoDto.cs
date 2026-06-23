using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class ElementoInspeccionadoDto
{
    [Required(ErrorMessage = "El campo Línea es obligatorio.")]
    public string Linea { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Isométrico es obligatorio.")]
    public string Isometrico { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Elemento es obligatorio.")]
    public string Elemento { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Spool es obligatorio.")]
    public string Spool { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Cuño es obligatorio.")]
    public string Cuno { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Espesor/Schedule es obligatorio.")]
    public string EspSch { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Diámetro es obligatorio.")]
    public string Diam { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Criterio es obligatorio.")]
    public string Criterio { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Resultado es obligatorio.")]
    [RegularExpression("^(Aprobado|Rechazado|N/A)$", ErrorMessage = "El resultado debe ser exactamente 'Aprobado', 'Rechazado' o 'N/A'")]
    public string Resultado { get; set; } = string.Empty;
}
