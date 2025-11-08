using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Tracks;

public class MentorTrackLevelConfiguration : IEntityTypeConfiguration<MentorTrackLevel>
{
    public void Configure(EntityTypeBuilder<MentorTrackLevel> builder)
    {
        builder.HasKey(mtl => mtl.Id);

        // Indexes
        builder.HasIndex(mtl => mtl.MentorProfileId);
        builder.HasIndex(mtl => mtl.TrackId);
        builder.HasIndex(mtl => new { mtl.MentorProfileId, mtl.TrackId, mtl.Level })
            .IsUnique();

        // Relationships
        builder.HasOne(mtl => mtl.MentorProfile)
            .WithMany(mp => mp.MentorTrackLevels)
            .HasForeignKey(mtl => mtl.MentorProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
