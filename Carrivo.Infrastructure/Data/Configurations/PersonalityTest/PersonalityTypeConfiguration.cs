using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityTypeConfiguration : IEntityTypeConfiguration<PersonalityType>
{
    public void Configure(EntityTypeBuilder<PersonalityType> builder)
    {
        builder.HasKey(pt => pt.Id);

        // Properties
        builder.Property(pt => pt.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pt => pt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(pt => pt.Code)
            .IsUnique();

        builder.HasIndex(pt => pt.IsActive);
    }
}
