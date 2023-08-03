using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankSystem.APPDBCONTEXT
{
    public class AppDbContext :DbContext
    {
        public DbSet<Customer> customers { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<CustomerTransaction> customerTransactions { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }
}
