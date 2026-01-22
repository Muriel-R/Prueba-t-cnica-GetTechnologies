using GetechnologiesTest.Domain;
using Microsoft.EntityFrameworkCore;

namespace GetechnologiesTest.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas => Set<Persona>();
        public DbSet<Factura> Facturas => Set<Factura>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Persona
            modelBuilder.Entity<Persona>(e =>
            {
                e.ToTable("Persona", "dbo");
                e.HasKey(x => x.PersonaId);

                e.Property(x => x.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(x => x.ApellidoPaterno)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(x => x.ApellidoMaterno)
                    .HasMaxLength(200);

                e.Property(x => x.Identificacion)
                    .IsRequired()
                    .HasMaxLength(50);

                e.HasIndex(x => x.Identificacion)
                    .IsUnique();
            });

            // Factura
            modelBuilder.Entity<Factura>(e =>
            {
                e.ToTable("Factura", "dbo");
                e.HasKey(x => x.FacturaId);

                e.Property(x => x.Monto)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.Fecha)
                    .IsRequired();

                e.HasOne(x => x.Persona)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(x => x.PersonaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
