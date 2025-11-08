using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityTypeTrackConfiguration : IEntityTypeConfiguration<PersonalityTypeTrack>
{
    public void Configure(EntityTypeBuilder<PersonalityTypeTrack> builder)
    {
        builder.HasKey(ptt => ptt.Id);

        // Properties
        builder.Property(ptt => ptt.CompatibilityScore)
            .HasPrecision(5, 2);

        // Indexes
        builder.HasIndex(ptt => ptt.PersonalityTypeId);
        builder.HasIndex(ptt => ptt.TrackId);
        builder.HasIndex(ptt => new { ptt.PersonalityTypeId, ptt.TrackId })
            .IsUnique();

        // Relationships
        builder.HasOne(ptt => ptt.PersonalityType)
            .WithMany(pt => pt.PersonalityTypeTracks)
            .HasForeignKey(ptt => ptt.PersonalityTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ptt => ptt.Track)
            .WithMany(t => t.PersonalityTypeTracks)
            .HasForeignKey(ptt => ptt.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
