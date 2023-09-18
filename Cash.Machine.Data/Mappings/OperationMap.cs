using Cash.Machine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cash.Machine.Data.EntitiesMappings
{
    public class OperationMap : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.Property(operation => operation.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(operation => operation.Description)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.ToTable("Operation");
            builder.Property(operation => operation.Id).HasColumnName("Id");
            builder.Property(operation => operation.Description).HasColumnName("Description");
        }
    }
}