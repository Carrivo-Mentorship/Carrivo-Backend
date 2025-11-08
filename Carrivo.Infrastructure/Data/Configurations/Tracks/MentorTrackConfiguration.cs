using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Tracks;

public class MentorTrackConfiguration : IEntityTypeConfiguration<MentorTrack>
{
    public void Configure(EntityTypeBuilder<MentorTrack> builder)
    {
        builder.HasKey(mt => mt.Id);

        // Indexes
        builder.HasIndex(mt => mt.MentorProfileId);
        builder.HasIndex(mt => mt.TrackId);
        builder.HasIndex(mt => new { mt.MentorProfileId, mt.TrackId })
            .IsUnique();

        // Relationships
        builder.HasOne(mt => mt.MentorProfile)
            .WithMany(mp => mp.MentorTracks)
            .HasForeignKey(mt => mt.MentorProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mt => mt.Track)
            .WithMany(t => t.MentorTracks)
            .HasForeignKey(mt => mt.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
