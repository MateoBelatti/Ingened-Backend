using Core.DTOs;
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
        var user = await _userService.ValidateCredentialsAsync(email, password);
        
        if (user == null)
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
                user = await _userService.GetByEmailAsync(payload.Email);
                
                if (user != null)
                {
                    await _userService.UpdateGoogleIdAsync(user.Id, payload.Subject);
                    user.GoogleId = payload.Subject;
                }
                else
                {
                    var createDto = new UserCreateDTO
                    {
                        Nombre = payload.Name,
                        Email = payload.Email,
                        Password = "" 
                    };
                    user = await _userService.AddAsync(createDto);
                    
                    await _userService.UpdateGoogleIdAsync(user.Id, payload.Subject);
                    user.GoogleId = payload.Subject;
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
