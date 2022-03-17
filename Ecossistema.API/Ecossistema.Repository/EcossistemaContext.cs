using Ecossistema.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecossistema.Data
{
    public class EcossistemaContext : DbContext
    {
        public EcossistemaContext(DbContextOptions<EcossistemaContext> options) : base(options) { }

        public DbSet<FaleConoscoSetor> FaleConoscos { get; set; }
        public DbSet<FaleConoscoSetor> FaleConoscoSetores { get; set; }
        public DbSet<FaleConoscoSetor> FaleConoscoSetoresContatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaleConosco>()
                .ToTable("FaleConosco", "dbo");

            modelBuilder.Entity<FaleConoscoSetor>()
                .ToTable("FaleConoscoSetor", "dbo");

            modelBuilder.Entity<FaleConoscoSetorContato>()
                .ToTable("FaleConoscoSetorContato", "dbo");
        }
    }
}