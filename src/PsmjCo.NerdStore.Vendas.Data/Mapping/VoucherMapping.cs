namespace PsmjCo.NerdStore.Vendas.Data.Mapping
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.DataUtilizacao)
                .HasColumnType("datetime");

            builder.Property(c => c.DataValidade)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.Percentual)
                .HasColumnType("decimal");

            builder.Property(c => c.TipoDescontoVoucher)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Utilizado)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.ValorDesconto)
                .HasColumnType("decimal");

            // 1 : N => Voucher : Pedidos
            builder.HasMany(c => c.Pedidos)
                .WithOne(c => c.Voucher)
                .HasForeignKey(c => c.VoucherId);

            builder.ToTable("Vouchers");
        }
    }
}