using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.User;

public class UserUpdateDTO
{
    [MaxLength(100)]
    public string? Nombre { get; set; }

    [EmailAddress]
    [MaxLength(150)]
    public string? Email { get; set; }
}
