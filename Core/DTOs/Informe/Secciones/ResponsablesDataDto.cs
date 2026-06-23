using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class ResponsablesDataDto
{
    [Required(ErrorMessage = "El campo Realizó es obligatorio.")]
    public string Realizo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Firma de quien realizó es obligatorio.")]
    public string FirmaRealizo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Revisó es obligatorio.")]
    public string Reviso { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Firma de quien revisó es obligatorio.")]
    public string FirmaReviso { get; set; } = string.Empty;
}
