using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSolve.Domain.Entities;

namespace TechSolve.DataModel.Configurations;

public class EnquiryConfiguration : IEntityTypeConfiguration<Enquiry>
{
    public void Configure(EntityTypeBuilder<Enquiry> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.TicketId).IsRequired().HasMaxLength(16);
        builder.HasIndex(e => e.TicketId).IsUnique();
        builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Phone).HasMaxLength(30);
        builder.Property(e => e.Company).HasMaxLength(150);
        builder.Property(e => e.ServiceSlug).IsRequired().HasMaxLength(100);
        builder.Property(e => e.ServiceName).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Message).IsRequired().HasMaxLength(5000);
        builder.Property(e => e.SourcePageUrl).HasMaxLength(1000);
        builder.HasIndex(e => e.Email);
        builder.HasIndex(e => e.ServiceSlug);
        builder.HasIndex(e => e.Status);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
