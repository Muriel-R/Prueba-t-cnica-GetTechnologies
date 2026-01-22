using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetechnologiesTest.Domain;

namespace GetechnologiesTest.Application.Abstractions
{
    public interface IFacturaRepository
    {
        Task<int> AddAsync(Factura factura);
        Task<List<Factura>> GetByPersonaIdAsync(int personaId);
    }
}
