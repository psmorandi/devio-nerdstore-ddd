namespace PsmjCo.NerdStore.Catalogo.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Data;
    using Core.Messages;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public async Task<bool> Commit()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added) entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified) entry.Property("DataCadastro").IsModified = false;
            }

            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }
    }
}