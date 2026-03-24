using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_EF_VM.Models;

namespace MVC_EF_VM.Data
{
    public class MVC_EF_VMContext : DbContext
    {
        public MVC_EF_VMContext (DbContextOptions<MVC_EF_VMContext> options)
            : base(options)
        {
        }

        public DbSet<MVC_EF_VM.Models.Investment> Investment { get; set; } = default!;
        //public
    }
}
