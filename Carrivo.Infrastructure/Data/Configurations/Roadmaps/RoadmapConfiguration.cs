using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class RoadmapConfiguration : IEntityTypeConfiguration<Roadmap>
{
    public void Configure(EntityTypeBuilder<Roadmap> builder)
    {
        builder.HasKey(r => r.Id);

        // Properties
        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(r => r.TrackId);
        builder.HasIndex(r => r.Level);
        builder.HasIndex(r => r.CreatorType);
        builder.HasIndex(r => new { r.TrackId, r.Level });

        // Relationships
        builder.HasOne(r => r.Track)
            .WithMany(t => t.Roadmaps)
            .HasForeignKey(r => r.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
