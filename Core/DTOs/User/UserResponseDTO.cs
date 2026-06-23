namespace Core.DTOs;

public class UserResponseDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? UltimaConeccion { get; set; }
    public string? GoogleId { get; set; }
}
