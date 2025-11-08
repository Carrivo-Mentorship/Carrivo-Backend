using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Users;

public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.HasKey(sp => sp.Id);

        // Properties
        builder.Property(sp => sp.Education)
            .HasMaxLength(500);

        builder.Property(sp => sp.Experience)
            .HasMaxLength(2000);

        builder.Property(sp => sp.CvUrl)
            .HasMaxLength(500);

        builder.Property(sp => sp.CurrentSkills)
            .HasMaxLength(1000);

        builder.Property(sp => sp.PreferredFields)
            .HasMaxLength(1000);

        builder.Property(sp => sp.DailyStudyHours)
            .HasPrecision(5, 2);

        // Indexes
        builder.HasIndex(sp => sp.UserId)
            .IsUnique();

        builder.HasIndex(sp => sp.IsDeleted);

        // Relationships
        builder.HasOne(sp => sp.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<StudentProfile>(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
