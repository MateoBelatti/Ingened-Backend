using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities;

public class Informe
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string NrInf { get; set; } = string.Empty;

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public string Url { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Cliente { get; set; } = string.Empty;

    [MaxLength(100)]
    public string GoogleDriveFileId { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }

    // Relación con el usuario que creó/es dueño del informe
    [ForeignKey(nameof(UserId))]
    [JsonIgnore]
    public User User { get; set; } = null!;
}
