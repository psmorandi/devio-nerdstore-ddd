namespace PsmjCo.NerdStore.Vendas.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.Data;
    using Core.Messages;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class VendasContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler mediatorHandler;

        public VendasContext(DbContextOptions<VendasContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            this.mediatorHandler = mediatorHandler;
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
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
            if(sucesso) await mediatorHandler.PublicarEventos(this);

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }
    }
}