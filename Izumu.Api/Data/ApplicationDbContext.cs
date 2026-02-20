using Microsoft.EntityFrameworkCore;
using Izumu.Shared.Models;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<Cliente> Clientes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => new { c.TipoDocumentoId, c.NumeroDocumento }).IsUnique();
    }
}