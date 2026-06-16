using Core.Entities;
using Core.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IGenerateJWT _generateJWT;
    private readonly string _googleClientId;

    public AuthService(IUserService userService, IGenerateJWT generateJWT, IConfiguration configuration)
    {
        _userService = userService;
        _generateJWT = generateJWT;
        _googleClientId = configuration["GoogleAuth:ClientId"] ?? string.Empty;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userService.GetByEmailAsync(email);
        
        // Verificación del hash de BCrypt
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return _generateJWT.GenerateToken(user);
    }

    public async Task<string?> GoogleLoginAsync(string idToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _googleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            
            var user = await _userService.GetByGoogleIdAsync(payload.Subject);
            
            if (user == null)
            {
                // Si no existe por GoogleId, buscar por Email
                user = await _userService.GetByEmailAsync(payload.Email);
                
                if (user != null)
                {
                    // Si existe por email, asociar el GoogleId
                    user.GoogleId = payload.Subject;
                    await _userService.UpdateAsync(user);
                }
                else
                {
                    // Crear nuevo usuario si no existe de ninguna forma
                    user = new User
                    {
                        Nombre = payload.Name,
                        Email = payload.Email,
                        GoogleId = payload.Subject,
                        Password = "" // UserService le aplicará un Guid y un Hash
                    };
                    await _userService.AddAsync(user);
                }
            }
            
            return _generateJWT.GenerateToken(user);
        }
        catch (InvalidJwtException)
        {
            return null;
        }
    }
}
