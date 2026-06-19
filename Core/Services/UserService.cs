using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
    }

    public async Task<UserResponseDTO?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO?> ValidateCredentialsAsync(string email, string password)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO?> GetByEmailAsync(string email)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == email);
        if (user == null) return null;
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO?> GetByGoogleIdAsync(string googleId)
    {
        var user = await _userRepository.GetByGoogleIdAsync(googleId);
        if (user == null) return null;
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task UpdateGoogleIdAsync(int userId, string googleId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user != null)
        {
            user.GoogleId = googleId;
            await _userRepository.UpdateAsync(user);
        }
    }

    public async Task<UserResponseDTO> AddAsync(UserCreateDTO dto)
    {
        var users = await _userRepository.GetAllAsync();
        if (users.Any(u => u.Email == dto.Email))
        {
            throw new BadRequestException("El email ya está en uso");
        }

        var user = _mapper.Map<User>(dto);
        
        if (string.IsNullOrEmpty(user.Password))
        {
            user.Password = Guid.NewGuid().ToString();
        }
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        
        var createdUser = await _userRepository.AddAsync(user);
        return _mapper.Map<UserResponseDTO>(createdUser);
    }

    public async Task UpdateAsync(int id, UserUpdateDTO dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("El usuario no existe");
        }

        if (!string.IsNullOrEmpty(dto.Nombre))
            user.Nombre = dto.Nombre;
            
        if (!string.IsNullOrEmpty(dto.Email))
        {
            var users = await _userRepository.GetAllAsync();
            if (users.Any(u => u.Email == dto.Email && u.Id != id))
            {
                throw new BadRequestException("El email ya está en uso por otro usuario");
            }
            user.Email = dto.Email;
        }

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("El usuario no existe");
        }

        await _userRepository.DeleteAsync(id);
    }
}
