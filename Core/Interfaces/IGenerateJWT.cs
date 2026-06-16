using Core.Entities;

namespace Core.Interfaces;

public interface IGenerateJWT
{
    string GenerateToken(User user);
}
