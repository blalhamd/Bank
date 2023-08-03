using BankSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.ConfigurationEntitiesByFluentApi
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers").HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.address).HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.dateOfBirth).HasColumnName("DateOfBirth").IsRequired(false);
            builder.Property(x => x.gmail).HasColumnName("Gmail").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);
            builder.Property(x => x.phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(250).IsRequired();

            builder.HasOne(x => x.bank)
                   .WithMany(x => x.customers)
                   .HasForeignKey(x => x.BandId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
