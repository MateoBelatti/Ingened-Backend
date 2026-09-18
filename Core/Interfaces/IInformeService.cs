using Core.DTOs.InformeLp;
using Core.Entities;

namespace Core.Interfaces;

public interface IInformeService
{
    Task<Informe> GenerarLpAsync(InformeLpDto dto, int userId);
    Task<IEnumerable<Informe>> GetAllInformesAsync();
    Task<Informe?> GetInformeByIdAsync(int id);
}