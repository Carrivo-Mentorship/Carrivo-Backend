using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Mentorship;

public class MentorshipRequestConfiguration : IEntityTypeConfiguration<MentorshipRequest>
{
    public void Configure(EntityTypeBuilder<MentorshipRequest> builder)
    {
        builder.HasKey(mr => mr.Id);

        // Properties
        builder.Property(mr => mr.Message)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(mr => mr.ResponseMessage)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(mr => mr.StudentProfileId);
        builder.HasIndex(mr => mr.MentorProfileId);
        builder.HasIndex(mr => mr.TrackId);
        builder.HasIndex(mr => mr.Status);
        builder.HasIndex(mr => new { mr.StudentProfileId, mr.Status });
        builder.HasIndex(mr => new { mr.MentorProfileId, mr.Status });

        // Relationships
        builder.HasOne(mr => mr.StudentProfile)
            .WithMany(sp => sp.MentorshipRequests)
            .HasForeignKey(mr => mr.StudentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.MentorProfile)
            .WithMany(mp => mp.MentorshipRequests)
            .HasForeignKey(mr => mr.MentorProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.Track)
            .WithMany()
            .HasForeignKey(mr => mr.TrackId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.MentorshipSession)
            .WithOne(ms => ms.MentorshipRequest)
            .HasForeignKey<MentorshipSession>(ms => ms.MentorshipRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
