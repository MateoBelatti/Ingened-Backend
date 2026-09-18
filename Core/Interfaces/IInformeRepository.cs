using Core.Entities;

namespace Core.Interfaces;

public interface IInformeRepository
{
    Task<Informe> CreateInformeAsync(string nrInf, string cliente, string url, string googleDriveFileId, int userId, string tipo);
    Task<IEnumerable<Informe>> GetAllInformesAsync();
    Task<Informe?> GetInformeByIdAsync(int id);
}
