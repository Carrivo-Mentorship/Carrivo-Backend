using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Users;

public class MentorProfileConfiguration : IEntityTypeConfiguration<MentorProfile>
{
    public void Configure(EntityTypeBuilder<MentorProfile> builder)
    {
        builder.HasKey(mp => mp.Id);

        // Properties
        builder.Property(mp => mp.JobTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mp => mp.ProfessionalBio)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(mp => mp.AvailabilitySchedule)
            .HasMaxLength(1000);

        builder.Property(mp => mp.PricePerHour)
            .HasPrecision(10, 2);

        builder.Property(mp => mp.SpokenLanguages)
            .HasMaxLength(500);

        builder.Property(mp => mp.AverageRating)
            .HasPrecision(3, 2);

        // Indexes
        builder.HasIndex(mp => mp.UserId)
            .IsUnique();

        builder.HasIndex(mp => mp.AverageRating);
        builder.HasIndex(mp => mp.IsDeleted);

        // Relationships
        builder.HasOne(mp => mp.User)
            .WithOne(u => u.MentorProfile)
            .HasForeignKey<MentorProfile>(mp => mp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
