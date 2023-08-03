using BankSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.ConfigurationEntitiesByFluentApi
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions").HasKey(x => x.Id);
            builder.Property(x => x.amount).HasPrecision(15, 2).IsRequired();
            builder.Property(x => x.dateTime).IsRequired(false);

            builder.HasMany(x => x.Customers)
                   .WithMany(x => x.Transactions)
                   .UsingEntity<CustomerTransaction>();

        }
    }
}
