using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class StudentProgressConfiguration : IEntityTypeConfiguration<StudentProgress>
{
    public void Configure(EntityTypeBuilder<StudentProgress> builder)
    {
        builder.HasKey(sp => sp.Id);

        // Properties
        builder.Property(sp => sp.TimeSpentHours)
            .HasPrecision(10, 2);

        builder.Property(sp => sp.Notes)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(sp => sp.StudentProfileId);
        builder.HasIndex(sp => sp.MilestoneTaskId);
        builder.HasIndex(sp => sp.IsCompleted);
        builder.HasIndex(sp => new { sp.StudentProfileId, sp.MilestoneTaskId })
            .IsUnique();
        builder.HasIndex(sp => new { sp.StudentProfileId, sp.IsCompleted });

        // Relationships
        builder.HasOne(sp => sp.StudentProfile)
            .WithMany(s => s.StudentProgress)
            .HasForeignKey(sp => sp.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sp => sp.MilestoneTask)
            .WithMany(mt => mt.StudentProgress)
            .HasForeignKey(sp => sp.MilestoneTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
