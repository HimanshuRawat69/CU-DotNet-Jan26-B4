using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseAPI.Config
{
    public class FluentApiConfig: IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Summary).HasMaxLength(500);
            builder.Property(x => x.Price).HasPrecision(18, 2);
            builder.Property(x => x.DiscountedPrice).HasPrecision(18, 2);
        }
    }
}
