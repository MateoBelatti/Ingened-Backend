using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class ParametrosLPDto
{
    [Required(ErrorMessage = "El campo Tipo de Penetrante es obligatorio.")]
    public string TipoPenetrante { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Tipo de Revelador es obligatorio.")]
    public string TipoRevelador { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Tipo de Removedor es obligatorio.")]
    public string TipoRemovedor { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Extensión de Ensayo es obligatorio.")]
    public string ExtensionEnsayo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Limpieza Inicial es obligatorio.")]
    public string LimpiezaInicial { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Aplicación de Penetrante es obligatorio.")]
    public string AplicacionPenetrante { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Tiempo de Penetración es obligatorio.")]
    public string TiempoPenetracion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Remoción de Exceso es obligatorio.")]
    public string RemocionExceso { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Tiempo de Secado es obligatorio.")]
    public string TiempoSecado { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Aplicación de Revelador es obligatorio.")]
    public string AplicacionRevelador { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Tiempo de Revelado es obligatorio.")]
    public string TiempoRevelado { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Limpieza Post Examen es obligatorio.")]
    public string LimpiezaPostExamen { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Temperatura es obligatorio.")]
    public string Temperatura { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Iluminación es obligatorio.")]
    public string Iluminacion { get; set; } = string.Empty;
}
