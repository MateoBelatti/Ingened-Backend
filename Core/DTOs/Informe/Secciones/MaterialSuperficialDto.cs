using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class MaterialSuperficialDto
{
    [Required(ErrorMessage = "El campo Material es obligatorio.")]
    public string Material { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Forma del Material es obligatorio.")]
    public string FormaMaterial { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Condiciones Superficiales es obligatorio.")]
    public string CondSuperficiales { get; set; } = string.Empty;
}
