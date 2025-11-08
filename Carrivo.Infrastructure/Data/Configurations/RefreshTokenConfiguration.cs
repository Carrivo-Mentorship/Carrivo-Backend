using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token)
            .IsRequired();

        builder.Property(r => r.CreatedByIp)
            .HasMaxLength(50);

        builder.Property(r => r.RevokedByIp)
            .HasMaxLength(50);

        builder.Property(r => r.RevocationReason)
            .HasMaxLength(200);

        // Indexes for performance
        builder.HasIndex(r => r.Token)
            .IsUnique();
        
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => new { r.UserId, r.IsRevoked, r.ExpiresAt });

        // Relationship
        builder.HasOne(r => r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
