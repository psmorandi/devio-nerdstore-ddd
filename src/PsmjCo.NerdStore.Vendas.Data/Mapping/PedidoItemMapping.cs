namespace PsmjCo.NerdStore.Vendas.Data.Mapping
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProdutoId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(c => c.ProdutoNome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.ValorUnitario)
                .IsRequired()
                .HasColumnType("decimal");


            // 1 : N => Pedido : PedidoItems
            builder.HasOne(c => c.Pedido)
                .WithMany(c => c.PedidoItems);

            builder.ToTable("PedidoItems");
        }
    }
}