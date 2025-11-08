using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityTestConfiguration : IEntityTypeConfiguration<Core.Entities.PersonalityTest>
{
    public void Configure(EntityTypeBuilder<Core.Entities.PersonalityTest> builder)
    {
        builder.HasKey(pt => pt.Id);

        // Properties
        builder.Property(pt => pt.Version)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(pt => pt.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(pt => pt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(pt => pt.Version);
    }
}
