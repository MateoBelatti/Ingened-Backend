using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface IInformeService
{
    Task<Informe> GenerarYGuardarInformeAsync(InformeDTO informeDto, int userId);
    Task<IEnumerable<Informe>> GetAllInformesAsync();
    Task<Informe?> GetInformeByIdAsync(int id);
}
