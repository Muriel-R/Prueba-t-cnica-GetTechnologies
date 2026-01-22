using GetechnologiesTest.Domain;

namespace GetechnologiesTest.Application.Abstractions
{
    public interface IPersonaRepository
    {
        Task<int> AddAsync(Persona persona);
        Task<Persona?> GetByIdAsync(int personaId);
        Task<List<Persona>> GetAllAsync();
        Task DeleteAsync(int personaId); 
    }
}
