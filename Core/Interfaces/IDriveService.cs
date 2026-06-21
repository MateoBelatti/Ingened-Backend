using Core.DTOs;

namespace Core.Interfaces;

public class DriveUploadResult
{
    public string FileId { get; set; } = string.Empty;
    public string WebViewLink { get; set; } = string.Empty;
}

public interface IDriveService
{
    Task<DriveUploadResult> UploadPdfAsync(byte[] fileBytes, DatosArchivosDto datosArchivos);
}
