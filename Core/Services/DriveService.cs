using Core.DTOs;
using Core.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace Core.Services;

public class DriveService : IDriveService
{
    private readonly IConfiguration _configuration;

    public DriveService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<DriveUploadResult> UploadPdfAsync(byte[] fileBytes, DatosArchivosDto datosArchivos)
    {
        var credentialsFileName = _configuration["GoogleDrive:CredentialsFileName"] ?? "client_secret.json";
        var credentialsFile = Path.Combine(AppContext.BaseDirectory, credentialsFileName);
        
        // Soporte para Railway: Leer el contenido del JSON desde variable de entorno y guardarlo como archivo
        var clientSecretJson = Environment.GetEnvironmentVariable("GoogleDrive__ClientSecretJson");
        if (!string.IsNullOrEmpty(clientSecretJson))
        {
            File.WriteAllText(credentialsFile, clientSecretJson);
        }

        var folderId = _configuration["GoogleDrive:FolderId"];

        if (string.IsNullOrEmpty(folderId))
        {
            throw new Exception("El FolderId de Google Drive no está configurado.");
        }

        // 1. Autenticación (OAuth 2.0 de Usuario)
        UserCredential credential;
        string credPath = Path.Combine(AppContext.BaseDirectory, "token.json");
        
        // Soporte para Railway: Leer el token de respuesta desde variable de entorno y guardarlo
        var tokenJson = Environment.GetEnvironmentVariable("GoogleDrive__TokenJson");
        if (!string.IsNullOrEmpty(tokenJson))
        {
            Directory.CreateDirectory(credPath);
            File.WriteAllText(Path.Combine(credPath, "Google.Apis.Auth.OAuth2.Responses.TokenResponse-user"), tokenJson);
        }
        using (var stream = new FileStream(credentialsFile, FileMode.Open, FileAccess.Read))
        {
            // Autoriza y guarda el token de refresco localmente
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                new[] { Google.Apis.Drive.v3.DriveService.ScopeConstants.DriveFile },
                "user",
                CancellationToken.None,
                new Google.Apis.Util.Store.FileDataStore(credPath, true));
        }

        // 2. Inicializar el servicio de Drive
        var service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Ingened API"
        });

        // 3. Crear los metadatos del archivo
        // Armamos el nombre según los requerimientos: Ej: Informe-YPF-ING-DID-LP-005.pdf
        var fileName = $"Informe_{datosArchivos.Cliente}_{datosArchivos.NrInf}.pdf".Replace(" ", "_");

        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = fileName,
            Parents = new List<string> { folderId }
        };

        // 4. Subir el archivo desde el flujo de memoria (MemoryStream)
        using var memoryStream = new MemoryStream(fileBytes);
        var request = service.Files.Create(fileMetadata, memoryStream, "application/pdf");
        // Solicitamos que nos devuelva el ID y el enlace para verlo
        request.Fields = "id, webViewLink";

        var progress = await request.UploadAsync();

        if (progress.Status == Google.Apis.Upload.UploadStatus.Failed)
        {
            throw new Exception($"Fallo al subir archivo a Google Drive: {progress.Exception?.Message}", progress.Exception);
        }

        var uploadedFile = request.ResponseBody;

        return new DriveUploadResult
        {
            FileId = uploadedFile.Id,
            WebViewLink = uploadedFile.WebViewLink
        };
    }
}
