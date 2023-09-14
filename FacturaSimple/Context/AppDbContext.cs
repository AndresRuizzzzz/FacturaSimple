using Microsoft.EntityFrameworkCore;
using FacturaSimple.Models;
namespace FacturaSimple.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FacturaDet>()
                .Property(x => x.Precio)
                .HasColumnType("decimal(18, 2)"); 
            modelBuilder.Entity<Producto>()
                .Property(x => x.Precio)
                .HasColumnType("decimal(18, 2)"); 
        }

        public DbSet<FacturaCab> FacturaCabecera { get; set; }
        public DbSet<FacturaDet> FacturaDetalle { get; set; }

        public DbSet<Producto> Producto { get; set; }



    }
}
