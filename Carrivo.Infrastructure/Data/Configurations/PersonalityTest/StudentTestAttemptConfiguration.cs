using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class StudentTestAttemptConfiguration : IEntityTypeConfiguration<StudentTestAttempt>
{
    public void Configure(EntityTypeBuilder<StudentTestAttempt> builder)
    {
        builder.HasKey(sta => sta.Id);

        // Properties
        builder.Property(sta => sta.MlModelOutput)
            .HasMaxLength(4000);

        // Indexes
        builder.HasIndex(sta => sta.StudentProfileId);
        builder.HasIndex(sta => sta.PersonalityTestId);
        builder.HasIndex(sta => sta.PersonalityTypeId);
        builder.HasIndex(sta => sta.IsCompleted);
        builder.HasIndex(sta => new { sta.StudentProfileId, sta.IsCompleted });
        builder.HasIndex(sta => new { sta.StudentProfileId, sta.CompletedAt });

        // Relationships
        builder.HasOne(sta => sta.StudentProfile)
            .WithMany(sp => sp.TestAttempts)
            .HasForeignKey(sta => sta.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sta => sta.PersonalityTest)
            .WithMany(pt => pt.TestAttempts)
            .HasForeignKey(sta => sta.PersonalityTestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sta => sta.PersonalityType)
            .WithMany(pt => pt.StudentTestAttempts)
            .HasForeignKey(sta => sta.PersonalityTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
