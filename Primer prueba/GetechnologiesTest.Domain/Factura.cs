using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetechnologiesTest.Domain
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public int PersonaId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public Persona Persona { get; set; } = null!;
    }
}
