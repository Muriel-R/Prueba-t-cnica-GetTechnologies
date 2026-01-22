using System.ComponentModel.DataAnnotations;

namespace GetechnologiesTest.Api.Models;

public class PersonaCreateRequest
{
    [Required, MaxLength(200)]
    public string Nombre { get; set; } = null!;

    [Required, MaxLength(200)]
    public string ApellidoPaterno { get; set; } = null!;

    [MaxLength(200)]
    public string? ApellidoMaterno { get; set; }

    [Required, MaxLength(50)]
    public string Identificacion { get; set; } = null!;
}
