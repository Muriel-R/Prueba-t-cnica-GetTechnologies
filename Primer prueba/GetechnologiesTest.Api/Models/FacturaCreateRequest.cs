using System.ComponentModel.DataAnnotations;

namespace GetechnologiesTest.Api.Models;

public class FacturaCreateRequest
{
    [Required]
    public int PersonaId { get; set; }

    [Required]
    [Range(0.01, 999999999)]
    public decimal Monto { get; set; }

    public DateTime? Fecha { get; set; }
}
