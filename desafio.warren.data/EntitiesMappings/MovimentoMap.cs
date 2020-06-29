using desafio.warren.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio.warren.data.EntitiesMappings
{
    public class MovimentoMap : IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.Property(movimento => movimento.Id)
                   .ValueGeneratedOnAdd();
            builder.Property(movimento => movimento.IdConta)
                    .IsRequired();
            builder.Property(movimento => movimento.IdOperacao)
                    .IsRequired();
            builder.Property(movimento => movimento.Valor)
                    .IsRequired();
            builder.Property(movimento => movimento.Data)
                    .IsRequired();
            builder.Property(movimento => movimento.Data)
                    .HasMaxLength(48);

            builder.ToTable("Movimento");
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.IdConta).HasColumnName("idConta");
            builder.Property(t => t.IdOperacao).HasColumnName("idOperacao");
            builder.Property(t => t.Valor).HasColumnName("valor");
            builder.Property(t => t.Data).HasColumnName("data");
            builder.Property(t => t.CodigoBarras).HasColumnName("codigoBarras");

            builder.HasOne(movimento => movimento.Conta)
                   .WithMany(conta => conta.Movimentos)
                   .HasForeignKey(movimento => movimento.IdConta);

            builder.HasOne(movimento => movimento.Operacao)
                   .WithMany(operacao => operacao.Movimentos)
                   .HasForeignKey(movimento => movimento.IdOperacao);
        }
    }
}
