using Core.DTOs;

namespace Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO?> LoginAsync(string email, string password);
    Task<AuthResponseDTO?> GoogleLoginAsync(string idToken);
    Task<AuthResponseDTO?> RefreshTokenAsync(string refreshToken);
}
