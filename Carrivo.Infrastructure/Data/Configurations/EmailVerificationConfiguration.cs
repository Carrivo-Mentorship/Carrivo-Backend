using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations;

public class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
{
    public void Configure(EntityTypeBuilder<EmailVerification> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.OtpCode)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(e => e.VerificationType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.RequestedFromIp)
            .HasMaxLength(50);

        // Indexes for performance
        builder.HasIndex(e => e.Email);
        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => new { e.Email, e.VerificationType, e.IsUsed });
        builder.HasIndex(e => e.ExpiresAt);

        // Relationship
        builder.HasOne(e => e.User)
            .WithMany(u => u.EmailVerifications)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
