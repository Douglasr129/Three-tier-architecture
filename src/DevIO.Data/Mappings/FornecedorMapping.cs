
using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");
            builder.Property(x => x.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.HasOne(p => p.Endereco)
                .WithOne(e => e.Fornecedor);

            builder.HasMany(p => p.Produtos)
                .WithOne(f => f.Fornecedor)
                .HasForeignKey(e => e.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
