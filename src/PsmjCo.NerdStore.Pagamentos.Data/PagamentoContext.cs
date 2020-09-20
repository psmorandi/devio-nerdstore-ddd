namespace PsmjCo.NerdStore.Pagamentos.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Business;
    using Core.Communication.Mediator;
    using Core.Data;
    using Core.Messages;
    using Microsoft.EntityFrameworkCore;

    public class PagamentoContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler mediatorHandler;

        public PagamentoContext(DbContextOptions<PagamentoContext> options, IMediatorHandler rebusHandler)
            : base(options)
        {
            this.mediatorHandler = rebusHandler ?? throw new ArgumentNullException(nameof(rebusHandler));
        }

        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }


        public async Task<bool> Commit()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await this.mediatorHandler.PublicarEventos(this);

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            base.OnModelCreating(modelBuilder);
        }
    }
}
