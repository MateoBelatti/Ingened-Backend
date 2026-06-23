using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class DatosArchivosDto
{
    [Required(ErrorMessage = "El campo NrInf es obligatorio.")]
    public string NrInf { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Cliente es obligatorio.")]
    public string Cliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo OC (Orden de Compra) es obligatorio.")]
    public string Oc { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Rev (Revisión) es obligatorio.")]
    public string Rev { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
    public string Fecha { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Código es obligatorio.")]
    public string Codigo { get; set; } = string.Empty;
}
