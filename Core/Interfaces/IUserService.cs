using Core.DTOs;

namespace Core.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAllAsync();
    Task<UserResponseDTO?> GetByIdAsync(int id);
    
    // Métodos para Auth
    Task<UserResponseDTO?> ValidateCredentialsAsync(string email, string password);
    Task<UserResponseDTO?> GetByEmailAsync(string email);
    Task<UserResponseDTO?> GetByGoogleIdAsync(string googleId);
    Task UpdateGoogleIdAsync(int userId, string googleId);

    Task<UserResponseDTO> AddAsync(UserCreateDTO dto);
    Task UpdateAsync(int id, UserUpdateDTO dto);
    Task DeleteAsync(int id);
}
