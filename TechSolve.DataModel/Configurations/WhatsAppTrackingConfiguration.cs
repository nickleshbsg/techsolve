using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSolve.Domain.Entities;

namespace TechSolve.DataModel.Configurations;

public class WhatsAppTrackingConfiguration : IEntityTypeConfiguration<WhatsAppTracking>
{
    public void Configure(EntityTypeBuilder<WhatsAppTracking> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.SessionId).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PageUrl).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.ServiceSlug).HasMaxLength(100);
        builder.Property(e => e.ClickSource).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => e.CreatedAt);
        builder.HasIndex(e => e.ServiceSlug);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
