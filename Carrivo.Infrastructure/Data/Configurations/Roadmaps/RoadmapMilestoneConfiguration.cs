using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class RoadmapMilestoneConfiguration : IEntityTypeConfiguration<RoadmapMilestone>
{
    public void Configure(EntityTypeBuilder<RoadmapMilestone> builder)
    {
        builder.HasKey(rm => rm.Id);

        // Properties
        builder.Property(rm => rm.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(rm => rm.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(rm => rm.RoadmapId);
        builder.HasIndex(rm => new { rm.RoadmapId, rm.OrderIndex });

        // Relationships
        builder.HasOne(rm => rm.Roadmap)
            .WithMany(r => r.Milestones)
            .HasForeignKey(rm => rm.RoadmapId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
