using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class StudentRoadmapConfiguration : IEntityTypeConfiguration<StudentRoadmap>
{
    public void Configure(EntityTypeBuilder<StudentRoadmap> builder)
    {
        builder.HasKey(sr => sr.Id);

        // Properties
        builder.Property(sr => sr.ProgressPercentage)
            .HasPrecision(5, 2);

        builder.Property(sr => sr.DailyStudyHours)
            .HasPrecision(5, 2);

        builder.Property(sr => sr.CustomizationNotes)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(sr => sr.StudentProfileId);
        builder.HasIndex(sr => sr.RoadmapId);
        builder.HasIndex(sr => sr.CreatedByMentorId);
        builder.HasIndex(sr => sr.IsActive);
        builder.HasIndex(sr => new { sr.StudentProfileId, sr.IsActive });

        // Relationships
        builder.HasOne(sr => sr.StudentProfile)
            .WithMany(sp => sp.StudentRoadmaps)
            .HasForeignKey(sr => sr.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sr => sr.Roadmap)
            .WithMany(r => r.StudentRoadmaps)
            .HasForeignKey(sr => sr.RoadmapId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.CreatedByMentor)
            .WithMany()
            .HasForeignKey(sr => sr.CreatedByMentorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
