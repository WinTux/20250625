using Microsoft.EntityFrameworkCore;

namespace AdministradorDePlatos.Conexion
{
    public class PlatoDbContext : DbContext
    {
        public DbSet<Models.Plato> Platos { get; set; }
        public PlatoDbContext(DbContextOptions<PlatoDbContext> options) : base(options)
        {
        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Plato>().ToTable("Platos");
            base.OnModelCreating(modelBuilder);
        }
        */
    }
}
