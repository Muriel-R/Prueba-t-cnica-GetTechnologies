using GetechnologiesTest.Api.Models;
using GetechnologiesTest.Application.Services;
using GetechnologiesTest.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GetechnologiesTest.Api.Controllers;

[ApiController]
[Route("api/personas")]
public class PersonasController : ControllerBase
{
    private readonly DirectorioService _directorio;

    public PersonasController(DirectorioService directorio)
    {
        _directorio = directorio;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] PersonaCreateRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var persona = new Persona
        {
            Nombre = request.Nombre.Trim(),
            ApellidoPaterno = request.ApellidoPaterno.Trim(),
            ApellidoMaterno = string.IsNullOrWhiteSpace(request.ApellidoMaterno) ? null : request.ApellidoMaterno.Trim(),
            Identificacion = request.Identificacion.Trim()
        };

        var id = await _directorio.CrearAsync(persona);

        return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { PersonaId = id });
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var lista = await _directorio.ListarAsync();
        return Ok(lista);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var persona = await _directorio.ObtenerAsync(id);
        if (persona == null) return NotFound();
        return Ok(persona);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _directorio.EliminarAsync(id);
        return NoContent();
    }
}
