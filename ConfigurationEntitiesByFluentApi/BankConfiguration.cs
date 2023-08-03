using BankSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.ConfigurationEntitiesByFluentApi
{
    internal class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Banks").HasKey(x => x.Id);
            builder.Property(x => x.address).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.NumberBranches).HasColumnName("Number_Branches").HasColumnType("float").IsRequired();
            builder.Property(x => x.NameBank).HasColumnType("varchar").HasMaxLength(250).IsRequired();
                

          

        }
    }


}
