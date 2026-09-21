using Core.DTOs;
using Core.Interfaces;
using Core.Exceptions;
using Core.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IGenerateJWT _generateJWT;
    private readonly string _googleClientId;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserService userService,
        IUserRepository userRepository,
        IGenerateJWT generateJWT,
        IConfiguration configuration)
    {
        _userService = userService;
        _userRepository = userRepository;
        _generateJWT = generateJWT;
        _configuration = configuration;
        _googleClientId = configuration["GoogleAuth:ClientId"] ?? string.Empty;
    }

    public async Task<AuthResponseDTO?> LoginAsync(string email, string password)
    {
        var userDto = await _userService.ValidateCredentialsAsync(email, password);
        
        if (userDto == null)
        {
            throw new UnauthorizedException("Credenciales inválidas. Verifique su email y contraseña.");
        }

        var user = await _userRepository.GetByIdAsync(userDto.Id);
        if (user == null)
        {
            throw new UnauthorizedException("Usuario no encontrado.");
        }

        return await CreateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDTO?> GoogleLoginAsync(string idToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _googleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var allowedEmailsArray = _configuration.GetSection("AllowedEmails").Get<string[]>();
            var allowedEmailsRaw = _configuration["AllowedEmails"];

            var allowedEmails = allowedEmailsArray?.Length > 0
                ? allowedEmailsArray
                : (allowedEmailsRaw?.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? []);

            if (allowedEmails.Length > 0 && !allowedEmails.Contains(payload.Email, StringComparer.OrdinalIgnoreCase))
                throw new UnauthorizedException($"El email '{payload.Email}' no está autorizado para acceder. Contacte al administrador.");

            var userDto = await _userService.GetByGoogleIdAsync(payload.Subject);
            
            if (userDto == null)
            {
                userDto = await _userService.GetByEmailAsync(payload.Email);
                
                if (userDto != null)
                {
                    await _userService.UpdateGoogleIdAsync(userDto.Id, payload.Subject);
                }
                else
                {
                    var createDto = new UserCreateDTO
                    {
                        Nombre = payload.Name,
                        Email = payload.Email,
                        Password = "" 
                    };
                    userDto = await _userService.AddAsync(createDto);
                    
                    await _userService.UpdateGoogleIdAsync(userDto.Id, payload.Subject);
                }
            }
            
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new UnauthorizedException("Usuario no encontrado.");
            }

            return await CreateAuthResponseAsync(user);
        }
        catch (InvalidJwtException)
        {
            throw new BadRequestException("Token de Google inválido o expirado.");
        }
    }

    public async Task<AuthResponseDTO?> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new UnauthorizedException("Token de refresco no proporcionado.");
        }

        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

        if (user == null || user.RefreshTokenExpiryTime == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedException("Token de refresco inválido o expirado.");
        }

        return await CreateAuthResponseAsync(user);
    }

    private async Task<AuthResponseDTO> CreateAuthResponseAsync(User user)
    {
        var userDto = new UserResponseDTO
        {
            Id = user.Id,
            Nombre = user.Nombre,
            Email = user.Email,
            UltimaConeccion = user.UltimaConeccion
        };

        var accessToken = _generateJWT.GenerateToken(userDto);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userRepository.UpdateAsync(user);

        return new AuthResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
