using Core.DTOs.InformeLp;
using Core.Entities;
using Core.Interfaces;
using Core.utils.InformeLp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class InformeService : IInformeService
{
    private readonly IDriveService _driveService;
    private readonly IInformeRepository _informeRepository;
    private readonly ILogger<InformeService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InformeService(
        IDriveService driveService,
        IInformeRepository informeRepository,
        ILogger<InformeService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _driveService = driveService;
        _informeRepository = informeRepository;
        _serviceProvider = serviceProvider;
    }

    public async Task<Informe> GenerarLpAsync(InformeLpDto dto, int userId)
    {
        return await GenerarInternoAsync(dto, userId, "LP");
    }

    private async Task<Informe> GenerarInternoAsync<TDto>(TDto dto, int userId, string tipo) where TDto : class, IBaseInformeDto
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "El informe no puede ser nulo.");

        if (dto is InformeLpDto lpDto)
        {
            ValidarLpDto(lpDto);
        }

        var builder = _serviceProvider.GetKeyedService<IInformeDocumentBuilder>(tipo);
        if (builder == null)
            throw new NotSupportedException($"Tipo de informe '{tipo}' no registrado.");

        var pdfBytes = builder.GeneratePdf(dto);

        var uploadResult = await _driveService.UploadPdfAsync(pdfBytes, dto.DatosArchivos);
        _logger.LogInformation("PDF subido a Drive con FileId {FileId} para informe {Numero}", uploadResult.FileId, dto.DatosArchivos.NrInf);

        return await _informeRepository.CreateInformeAsync(
            dto.DatosArchivos.NrInf,
            dto.DatosArchivos.Cliente,
            uploadResult.WebViewLink,
            uploadResult.FileId,
            userId,
            tipo
        );
    }

    private void ValidarLpDto(InformeLpDto dto)
    {
        if (dto.Elementos == null || dto.Elementos.Count == 0)
            throw new ArgumentException("Debe haber al menos un elemento inspeccionado.", nameof(dto.Elementos));

        if (dto.Consumibles == null || dto.Consumibles.Count == 0)
            throw new ArgumentException("Debe registrar al menos un consumible.", nameof(dto.Consumibles));
    }

    public async Task<IEnumerable<Informe>> GetAllInformesAsync()
    {
        return await _informeRepository.GetAllInformesAsync();
    }

    public async Task<Informe?> GetInformeByIdAsync(int id)
    {
        return await _informeRepository.GetInformeByIdAsync(id);
    }
}