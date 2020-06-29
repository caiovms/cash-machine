using desafio.warren.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio.warren.data.EntitiesMappings
{
    public class OperacaoMap : IEntityTypeConfiguration<Operacao>
    {
        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            builder.Property(operacao => operacao.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(operacao => operacao.Descricao)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.ToTable("Operacao");
            builder.Property(operacao => operacao.Id).HasColumnName("id");
            builder.Property(operacao => operacao.Descricao).HasColumnName("descricao");
        }
    }
}
