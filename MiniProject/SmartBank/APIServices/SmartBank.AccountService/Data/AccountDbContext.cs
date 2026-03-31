using Microsoft.EntityFrameworkCore;
using SmartBank.AccountService.Models;

namespace SmartBank.AccountService.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasPrecision(18, 2); // 18 is the total digits, 2 is the decimal places

            modelBuilder.Entity<Transaction>()
                .Property(a => a.Amount)
                .HasPrecision(18, 2); // 18 is the total digits, 2 is the decimal places

            base.OnModelCreating(modelBuilder);
        }
    }
}
