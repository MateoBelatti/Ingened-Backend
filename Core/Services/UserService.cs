using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(u => u.Email == email);
    }

    public async Task<User?> GetByGoogleIdAsync(string googleId)
    {
        return await _userRepository.GetByGoogleIdAsync(googleId);
    }

    public async Task<User> AddAsync(User user)
    {
        // Generar una contraseña aleatoria si viene vacía (ej: login por Google)
        if (string.IsNullOrEmpty(user.Password))
        {
            user.Password = Guid.NewGuid().ToString();
        }
        
        // Hashear la contraseña antes de guardarla
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        
        return await _userRepository.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
