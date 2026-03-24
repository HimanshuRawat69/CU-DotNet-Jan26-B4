using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanManagementWebAPI.Models;

namespace LoanManagementWebAPI.Data
{
    public class LoanManagementWebAPIContext : DbContext
    {
        public LoanManagementWebAPIContext (DbContextOptions<LoanManagementWebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<LoanManagementWebAPI.Models.Loan> Loan { get; set; } = default!;
    }
}
