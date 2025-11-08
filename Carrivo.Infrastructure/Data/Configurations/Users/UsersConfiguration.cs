using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Users;

public class UsersConfiguration : IEntityTypeConfiguration<Core.Entities.Users>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Users> builder)
    {
        // Properties
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(u => u.LastLoginIp)
            .HasMaxLength(50);

        builder.Property(u => u.OAuthProvider)
            .HasMaxLength(50);

        builder.Property(u => u.OAuthProviderId)
            .HasMaxLength(255);

        // Indexes
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.UserType);
        builder.HasIndex(u => u.IsDeleted);
        builder.HasIndex(u => new { u.Email, u.IsDeleted });

    }
}
