using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class DatosGeneralesDto
{
    [Required(ErrorMessage = "El campo Proyecto es obligatorio.")]
    public string Proyecto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Componente es obligatorio.")]
    public string Componente { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Subconjunto es obligatorio.")]
    public string Subconjunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Obra es obligatorio.")]
    public string Obra { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Plano es obligatorio.")]
    public string Plano { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Posición es obligatorio.")]
    public string Posicion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Lugar es obligatorio.")]
    public string Lugar { get; set; } = string.Empty;
}
