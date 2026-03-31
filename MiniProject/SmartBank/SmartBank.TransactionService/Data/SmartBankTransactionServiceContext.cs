using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartBank.TransactionService.Models;

namespace SmartBank.TransactionService.Data
{
    public class SmartBankTransactionServiceContext : DbContext
    {
        public SmartBankTransactionServiceContext (DbContextOptions<SmartBankTransactionServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; } = default!;
    }
}
