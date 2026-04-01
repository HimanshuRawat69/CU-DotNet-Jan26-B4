using Microsoft.EntityFrameworkCore;
using Day69.Models;

namespace Day69.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configure Student Entity
            modelBuilder.Entity<Student>().ToTable("Students"); // Table name
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>().Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(s => s.Email)
                .IsRequired();
            modelBuilder.Entity<Student>().HasIndex(s => s.Email)
                .IsUnique();


            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            modelBuilder.Entity<Course>().Property(c => c.Title)
                .IsRequired();


            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentCourses"));
        }
    }
}