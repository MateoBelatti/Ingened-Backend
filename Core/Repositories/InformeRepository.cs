using Core.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class InformeRepository : IInformeRepository
{
    private readonly AppDbContext _context;

    public InformeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Informe> CreateInformeAsync(string nrInf, string cliente, string url, string googleDriveFileId, int userId)
    {
        var informe = new Informe
        {
            NrInf = nrInf ?? "S/N",
            Cliente = cliente ?? "S/N",
            Fecha = DateTime.UtcNow,
            Url = url,
            GoogleDriveFileId = googleDriveFileId,
            UserId = userId
        };

        _context.Informes.Add(informe);
        await _context.SaveChangesAsync();
        return informe;
    }

    public async Task<IEnumerable<Informe>> GetAllInformesAsync()
    {
        return await _context.Informes.ToListAsync();
    }

    public async Task<Informe?> GetInformeByIdAsync(int id)
    {
        return await _context.Informes.FindAsync(id);
    }
}
