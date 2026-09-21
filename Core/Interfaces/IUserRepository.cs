using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByGoogleIdAsync(string googleId);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}
