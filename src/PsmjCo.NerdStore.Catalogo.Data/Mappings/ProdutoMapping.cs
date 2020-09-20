namespace PsmjCo.NerdStore.Catalogo.Data.Mappings
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.QuantidadeEstoque)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.OwnsOne(
                c => c.Dimensoes,
                cm =>
                {
                    cm.Property(c => c.Altura)
                        .HasColumnName("Altura")
                        .HasColumnType("int");

                    cm.Property(c => c.Largura)
                        .HasColumnName("Largura")
                        .HasColumnType("int");

                    cm.Property(c => c.Profundidade)
                        .HasColumnName("Profundidade")
                        .HasColumnType("int");
                });

            builder.ToTable("Produtos");
        }
    }
}