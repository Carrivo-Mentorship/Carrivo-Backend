using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Mentorship;

public class MentorshipSessionConfiguration : IEntityTypeConfiguration<MentorshipSession>
{
    public void Configure(EntityTypeBuilder<MentorshipSession> builder)
    {
        builder.HasKey(ms => ms.Id);

        // Properties
        builder.Property(ms => ms.PaidAmount)
            .HasPrecision(10, 2);

        builder.Property(ms => ms.Review)
            .HasMaxLength(2000);

        builder.Property(ms => ms.Rating)
            .HasMaxLength(1); // 1-5 stars

        // Indexes
        builder.HasIndex(ms => ms.MentorshipRequestId)
            .IsUnique();

        builder.HasIndex(ms => ms.StudentProfileId);
        builder.HasIndex(ms => ms.MentorProfileId);
        builder.HasIndex(ms => ms.TrackId);
        builder.HasIndex(ms => ms.Status);
        builder.HasIndex(ms => new { ms.StudentProfileId, ms.Status });
        builder.HasIndex(ms => new { ms.MentorProfileId, ms.Status });
        builder.HasIndex(ms => ms.IsPaid);

        // Relationships
        builder.HasOne(ms => ms.StudentProfile)
            .WithMany(sp => sp.MentorshipSessions)
            .HasForeignKey(ms => ms.StudentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.MentorProfile)
            .WithMany(mp => mp.MentorshipSessions)
            .HasForeignKey(ms => ms.MentorProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.Track)
            .WithMany()
            .HasForeignKey(ms => ms.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
