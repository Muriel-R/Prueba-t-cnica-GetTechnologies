using GetechnologiesTest.Application.Abstractions;
using GetechnologiesTest.Domain;

namespace GetechnologiesTest.Application.Services;

public class VentasService
{
    private readonly IFacturaRepository _facturas;
    private readonly IPersonaRepository _personas;

    public VentasService(IFacturaRepository facturas, IPersonaRepository personas)
    {
        _facturas = facturas;
        _personas = personas;
    }

    public async Task<int> CrearFacturaAsync(Factura factura)
    {
        // valida que exista la persona
        var persona = await _personas.GetByIdAsync(factura.PersonaId);
        if (persona == null)
            throw new InvalidOperationException("La persona no existe.");

        return await _facturas.AddAsync(factura);
    }

    public Task<List<Factura>> ListarPorPersonaAsync(int personaId)
        => _facturas.GetByPersonaIdAsync(personaId);
}
