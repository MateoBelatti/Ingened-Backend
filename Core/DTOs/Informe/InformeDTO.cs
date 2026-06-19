using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class InformeDTO
{
    [Required(ErrorMessage = "Los datos del archivo son obligatorios.")]
    public DatosArchivosDto DatosArchivos { get; set; } = null!;

    [Required(ErrorMessage = "Los datos generales son obligatorios.")]
    public DatosGeneralesDto DatosGenerales { get; set; } = null!;

    [Required(ErrorMessage = "El procedimiento y las normas son obligatorios.")]
    public ProcedimientoNormasDto ProcedimientoNormas { get; set; } = null!;

    [Required(ErrorMessage = "Los datos del material son obligatorios.")]
    public MaterialSuperficialDto MaterialSuperficial { get; set; } = null!;

    [Required(ErrorMessage = "Los parámetros de líquidos penetrantes son obligatorios.")]
    public ParametrosLPDto ParametrosLP { get; set; } = null!;

    [Required(ErrorMessage = "Debe haber al menos un elemento inspeccionado.")]
    public List<ElementoInspeccionadoDto> Elementos { get; set; } = new();

    [Required(ErrorMessage = "El resultado global es obligatorio.")]
    public ResultadoGlobalDataDto ResultadoGlobal { get; set; } = null!;

    [Required(ErrorMessage = "Los responsables son obligatorios.")]
    public ResponsablesDataDto Responsables { get; set; } = null!;

    [Required(ErrorMessage = "Debe registrar al menos un consumible.")]
    public List<ConsumibleDto> Consumibles { get; set; } = new();

    // Las fotos pueden ser opcionales o mandatorias dependiendo de la lógica de negocio
    public RegistroFotograficoDataDto? RegistroFotografico { get; set; }
}
