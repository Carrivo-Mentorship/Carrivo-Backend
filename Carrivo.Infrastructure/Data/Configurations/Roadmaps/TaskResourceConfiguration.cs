using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class TaskResourceConfiguration : IEntityTypeConfiguration<TaskResource>
{
    public void Configure(EntityTypeBuilder<TaskResource> builder)
    {
        builder.HasKey(tr => tr.Id);

        // Properties
        builder.Property(tr => tr.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(tr => tr.Description)
            .HasMaxLength(1000);

        builder.Property(tr => tr.Url)
            .IsRequired()
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(tr => tr.MilestoneTaskId);
        builder.HasIndex(tr => tr.ResourceType);
        builder.HasIndex(tr => new { tr.MilestoneTaskId, tr.OrderIndex });

        // Relationships
        builder.HasOne(tr => tr.MilestoneTask)
            .WithMany(mt => mt.Resources)
            .HasForeignKey(tr => tr.MilestoneTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
