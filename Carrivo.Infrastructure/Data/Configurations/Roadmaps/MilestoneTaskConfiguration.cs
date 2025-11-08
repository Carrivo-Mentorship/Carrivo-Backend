using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class MilestoneTaskConfiguration : IEntityTypeConfiguration<MilestoneTask>
{
    public void Configure(EntityTypeBuilder<MilestoneTask> builder)
    {
        builder.HasKey(mt => mt.Id);

        // Properties
        builder.Property(mt => mt.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mt => mt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(mt => mt.RoadmapMilestoneId);
        builder.HasIndex(mt => new { mt.RoadmapMilestoneId, mt.OrderIndex });

        // Relationships
        builder.HasOne(mt => mt.RoadmapMilestone)
            .WithMany(rm => rm.Tasks)
            .HasForeignKey(mt => mt.RoadmapMilestoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
