namespace Core.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string email, string password);
    Task<string?> GoogleLoginAsync(string idToken);
}
