using Core.DTOs;

namespace Core.Interfaces;

public interface IGenerateJWT
{
    string GenerateToken(UserResponseDTO user);
}
