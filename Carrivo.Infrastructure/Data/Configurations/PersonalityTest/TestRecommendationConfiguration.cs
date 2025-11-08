using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class TestRecommendationConfiguration : IEntityTypeConfiguration<TestRecommendation>
{
    public void Configure(EntityTypeBuilder<TestRecommendation> builder)
    {
        builder.HasKey(tr => tr.Id);

        // Properties
        builder.Property(tr => tr.Score)
            .HasPrecision(5, 2);

        // Indexes
        builder.HasIndex(tr => tr.StudentTestAttemptId);
        builder.HasIndex(tr => tr.TrackId);
        builder.HasIndex(tr => new { tr.StudentTestAttemptId, tr.Rank });

        // Relationships
        builder.HasOne(tr => tr.StudentTestAttempt)
            .WithMany(sta => sta.TestRecommendations)
            .HasForeignKey(tr => tr.StudentTestAttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tr => tr.Track)
            .WithMany(t => t.TestRecommendations)
            .HasForeignKey(tr => tr.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
