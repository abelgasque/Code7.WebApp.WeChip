using Microsoft.EntityFrameworkCore;
using WeChip.Models;

namespace WeChip.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Oferta> Ofertas { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(e => new { e.Id });
            modelBuilder.Entity<Status>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Produto>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Oferta>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Endereco>().HasKey(c => new { c.Id });
        }

    }
}
