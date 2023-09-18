using Cash.Machine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cash.Machine.Data.EntitiesMappings
{
    public class MovementMap : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.Property(movement => movement.Id)
                   .ValueGeneratedOnAdd();
            builder.Property(movement => movement.AccountId)
                    .IsRequired();
            builder.Property(movement => movement.OperationId)
                    .IsRequired();
            builder.Property(movement => movement.Amount)
                    .IsRequired();
            builder.Property(movement => movement.Date)
                    .IsRequired();
            builder.Property(movement => movement.BarCode)
                    .HasMaxLength(48);

            builder.ToTable("Movement");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.AccountId).HasColumnName("AccountId");
            builder.Property(t => t.OperationId).HasColumnName("OperationId");
            builder.Property(t => t.Amount).HasColumnName("Amount");
            builder.Property(t => t.Date).HasColumnName("Date");
            builder.Property(t => t.BarCode).HasColumnName("BarCode");

            builder.HasOne(movement => movement.Account)
                   .WithMany(account => account.Movements)
                   .HasForeignKey(movement => movement.AccountId);

            builder.HasOne(movement => movement.Operation)
                   .WithMany(operation => operation.Movements)
                   .HasForeignKey(movement => movement.OperationId);
        }
    }
}
