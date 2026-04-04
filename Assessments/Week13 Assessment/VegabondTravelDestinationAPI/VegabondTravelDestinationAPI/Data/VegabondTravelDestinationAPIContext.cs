using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegabondTravelDestinationAPI.Models;

namespace VegabondTravelDestinationAPI.Data
{
    public class VegabondTravelDestinationAPIContext : DbContext
    {
        public VegabondTravelDestinationAPIContext (DbContextOptions<VegabondTravelDestinationAPIContext> options)
            : base(options)
        {
        }
        

        public DbSet<Destination> Destinations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(d => d.CityName)
                      .IsRequired();

                entity.Property(d => d.Country)
                      .IsRequired();

                entity.Property(d => d.Description)
                      .HasMaxLength(200);

                entity.Property(d => d.Rating)
                      .HasDefaultValue(3);

            });
        }

    }
}
