using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSolve.Domain.Entities;

namespace TechSolve.DataModel.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Slug).IsRequired().HasMaxLength(200);
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.Property(e => e.Title).IsRequired().HasMaxLength(300);
        builder.Property(e => e.Category).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => e.IsPublished);
        builder.HasIndex(e => e.Category);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
