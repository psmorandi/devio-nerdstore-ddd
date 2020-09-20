namespace PsmjCo.NerdStore.Vendas.Data.Mapping
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.Property(p => p.ClienteId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.Desconto)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(p => p.PedidoStatus)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(p => p.VoucherUtilizado)
                .IsRequired()
                .HasColumnType("bit");

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.PedidoItems)
                .WithOne(c => c.Pedido)
                .HasForeignKey(c => c.PedidoId);

            builder.ToTable("Pedidos");
        }
    }
}