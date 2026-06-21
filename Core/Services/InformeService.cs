using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.utils.InformeLp;
using QuestPDF.Fluent;

namespace Core.Services;

public class InformeService : IInformeService
{
    private readonly IDriveService _driveService;
    private readonly IInformeRepository _informeRepository;

    public InformeService(IDriveService driveService, IInformeRepository informeRepository)
    {
        _driveService = driveService;
        _informeRepository = informeRepository;
    }

    public async Task<Informe> GenerarYGuardarInformeAsync(InformeDTO informeDto, int userId)
    {
        if (informeDto == null)
            throw new ArgumentNullException(nameof(informeDto), "El informe no puede ser nulo.");

        if (informeDto.Elementos == null || informeDto.Elementos.Count == 0)
            throw new ArgumentException("Debe haber al menos un elemento inspeccionado.", nameof(informeDto));

        if (informeDto.Consumibles == null || informeDto.Consumibles.Count == 0)
            throw new ArgumentException("Debe registrar al menos un consumible.", nameof(informeDto));
        
        // Genera el informe
        var document = new InformeLpDocument(informeDto);
        var pdfBytes = document.GeneratePdf();

        // Sube el informe a drive
        var uploadResult = await _driveService.UploadPdfAsync(pdfBytes, informeDto.DatosArchivos);

        // 3. Guardar en Base de Datos a través del repositorio
        return await _informeRepository.CreateInformeAsync(
            informeDto.DatosArchivos.NrInf,
            informeDto.DatosArchivos.Cliente,
            uploadResult.WebViewLink,
            uploadResult.FileId,
            userId
        );
    }
}
