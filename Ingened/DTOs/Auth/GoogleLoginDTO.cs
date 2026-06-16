using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Auth;

public class GoogleLoginDTO
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
}
