using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class GoogleLoginDTO
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
}
