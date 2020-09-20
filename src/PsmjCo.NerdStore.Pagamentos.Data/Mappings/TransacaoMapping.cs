namespace PsmjCo.NerdStore.Pagamentos.Data.Mappings
{
    using Business;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PedidoId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(c => c.StatusTransacao)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Total)
                .IsRequired()
                .HasColumnType("decimal");

            builder.ToTable("Transacoes");
        }
    }
}