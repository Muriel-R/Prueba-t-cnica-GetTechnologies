using GetechnologiesTest.Application.Abstractions;
using GetechnologiesTest.Domain;
using Microsoft.EntityFrameworkCore;

namespace GetechnologiesTest.Infrastructure.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly AppDbContext _context;

    public PersonaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Persona persona)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();
        return persona.PersonaId;
    }

    public Task<List<Persona>> GetAllAsync()
        => _context.Personas.AsNoTracking().ToListAsync();

    public Task<Persona?> GetByIdAsync(int personaId)
        => _context.Personas
            .Include(p => p.Facturas)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PersonaId == personaId);

    public async Task DeleteAsync(int personaId)
    {
        var persona = await _context.Personas.FirstOrDefaultAsync(p => p.PersonaId == personaId);
        if (persona == null) return;

        _context.Personas.Remove(persona);
        await _context.SaveChangesAsync(); 
    }
}
