using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Tracks;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(t => t.Code)
            .IsUnique();

        builder.HasIndex(t => t.IsActive);
    }
}
