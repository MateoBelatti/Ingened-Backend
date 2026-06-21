using Core.Entities;

namespace Core.Interfaces;

public interface IInformeRepository
{
    Task<Informe> CreateInformeAsync(string nrInf, string cliente, string url, string googleDriveFileId, int userId);
}
