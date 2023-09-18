using Cash.Machine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cash.Machine.Data.EntitiesMappings
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(account => account.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(account => account.Agency)
                    .IsRequired()
                    .HasMaxLength(3);
            builder.Property(account => account.Type)
                    .IsRequired()
                    .HasMaxLength(2);
            builder.Property(account => account.Number)
                    .IsRequired()
                    .HasMaxLength(8);
            builder.Property(account => account.Digit)
                    .IsRequired()
                    .HasMaxLength(1);
            builder.Property(account => account.CreatedOn)
                    .IsRequired();

            builder.ToTable("Account");
            builder.Property(account => account.Id).HasColumnName("Id");
            builder.Property(account => account.Agency).HasColumnName("Agency");
            builder.Property(account => account.Type).HasColumnName("Type");
            builder.Property(account => account.Number).HasColumnName("Number");
            builder.Property(account => account.Digit).HasColumnName("Digit");
            builder.Property(account => account.Balance).HasColumnName("Balance");
            builder.Property(account => account.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
