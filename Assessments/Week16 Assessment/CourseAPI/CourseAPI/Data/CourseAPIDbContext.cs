using CourseAPI.Config;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Data
{
    public class CourseAPIDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public CourseAPIDbContext(DbContextOptions<CourseAPIDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new FluentApiConfig().Configure(modelBuilder.Entity<Course>());
        }
    }
}