using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class ProcedimientoNormasDto
{
    [Required(ErrorMessage = "El campo Procedimiento General es obligatorio.")]
    public string ProcGeneral { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Procedimiento Específico es obligatorio.")]
    public string ProcEspecifico { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Tipo de Ensayo es obligatorio.")]
    public string EnsayoTipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Norma es obligatorio.")]
    public string Norma { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Código de Referencia es obligatorio.")]
    public string CodigoRef { get; set; } = string.Empty;
}
