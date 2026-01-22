using GetechnologiesTest.Application.Abstractions;
using GetechnologiesTest.Domain;
using Microsoft.EntityFrameworkCore;

namespace GetechnologiesTest.Infrastructure.Repositories;

public class FacturaRepository : IFacturaRepository
{
    private readonly AppDbContext _context;

    public FacturaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Factura factura)
    {
        _context.Facturas.Add(factura);
        await _context.SaveChangesAsync();
        return factura.FacturaId;
    }

    public Task<List<Factura>> GetByPersonaIdAsync(int personaId)
        => _context.Facturas
            .AsNoTracking()
            .Where(f => f.PersonaId == personaId)
            .OrderByDescending(f => f.Fecha)
            .ToListAsync();
}
