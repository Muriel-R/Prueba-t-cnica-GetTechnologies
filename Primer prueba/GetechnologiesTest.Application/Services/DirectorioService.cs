using GetechnologiesTest.Application.Abstractions;
using GetechnologiesTest.Domain;

namespace GetechnologiesTest.Application.Services;

public class DirectorioService
{
    private readonly IPersonaRepository _personas;

    public DirectorioService(IPersonaRepository personas)
    {
        _personas = personas;
    }

    public Task<int> CrearAsync(Persona persona) => _personas.AddAsync(persona);
    public Task<List<Persona>> ListarAsync() => _personas.GetAllAsync();
    public Task<Persona?> ObtenerAsync(int id) => _personas.GetByIdAsync(id);
    public Task EliminarAsync(int id) => _personas.DeleteAsync(id);
}
