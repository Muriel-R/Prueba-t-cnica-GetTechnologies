using GetechnologiesTest.Api.Models;
using GetechnologiesTest.Application.Services;
using GetechnologiesTest.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GetechnologiesTest.Api.Controllers;

[ApiController]
[Route("api/facturas")]
public class FacturasController : ControllerBase
{
    private readonly VentasService _ventas;

    public FacturasController(VentasService ventas)
    {
        _ventas = ventas;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] FacturaCreateRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        try
        {
            var factura = new Factura
            {
                PersonaId = request.PersonaId,
                Monto = request.Monto,
                Fecha = request.Fecha ?? DateTime.UtcNow
            };

            var id = await _ventas.CrearFacturaAsync(factura);
            return Ok(new { FacturaId = id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("persona/{personaId:int}")]
    public async Task<IActionResult> ListarPorPersona(int personaId)
    {
        var lista = await _ventas.ListarPorPersonaAsync(personaId);
        return Ok(lista);
    }
}
