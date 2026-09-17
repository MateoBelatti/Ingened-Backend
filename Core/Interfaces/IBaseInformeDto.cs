using Core.DTOs.Common;

namespace Core.Interfaces;

public interface IBaseInformeDto
{
    string Tipo { get; set; }
    DatosArchivosDto DatosArchivos { get; set; }
}
