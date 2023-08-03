using BankSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.ConfigurationEntitiesByFluentApi
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts").HasKey(x => x.Id);
            builder.Property(x => x.accountNumber).HasColumnName("Account_Number").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.balance).HasColumnName("Balance").HasPrecision(15, 2).IsRequired();

            builder.HasOne(x => x.bank)
                   .WithMany(x => x.accounts)
                   .HasForeignKey(x => x.bankId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.customer)
                   .WithOne(x => x.account)
                   .HasForeignKey<Account>(x => x.customerId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.customerId).IsUnique();
        }
    }
}
/*
 
 Introducing FOREIGN KEY constraint 'FK_Accounts_Customers_customerId' on table 'Accounts' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
Could not create constraint or index. See previous errors.
 */