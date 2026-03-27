using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartBank.Models;

namespace SmartBank.Data
{
    public class SmartBankContext : DbContext
    {
        public SmartBankContext (DbContextOptions<SmartBankContext> options)
            : base(options)
        {
        }

        public DbSet<SmartBank.Models.Account> Account { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
