using desafio.warren.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio.warren.data.EntitiesMappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.Property(conta => conta.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(conta => conta.Agencia)
                    .IsRequired()
                    .HasMaxLength(3);
            builder.Property(conta => conta.Tipo)
                    .IsRequired()
                    .HasMaxLength(2);
            builder.Property(conta => conta.Numero)
                    .IsRequired()
                    .HasMaxLength(8);
            builder.Property(conta => conta.Digito)
                    .IsRequired()
                    .HasMaxLength(1);
            builder.Property(conta => conta.DataCadastro)
                    .IsRequired();

            builder.ToTable("Conta");
            builder.Property(conta => conta.Id).HasColumnName("id");
            builder.Property(conta => conta.Agencia).HasColumnName("agencia");
            builder.Property(conta => conta.Tipo).HasColumnName("tipo");
            builder.Property(conta => conta.Numero).HasColumnName("numero");
            builder.Property(conta => conta.Digito).HasColumnName("digito");
            builder.Property(conta => conta.Saldo).HasColumnName("saldo");
            builder.Property(conta => conta.DataCadastro).HasColumnName("dataCadastro");
        }
    }
}
