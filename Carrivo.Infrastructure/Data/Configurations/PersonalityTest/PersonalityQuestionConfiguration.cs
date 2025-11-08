using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityQuestionConfiguration : IEntityTypeConfiguration<PersonalityQuestion>
{
    public void Configure(EntityTypeBuilder<PersonalityQuestion> builder)
    {
        builder.HasKey(pq => pq.Id);

        // Properties
        builder.Property(pq => pq.Question)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(pq => pq.OptionsJson)
            .HasMaxLength(4000);

        // Indexes
        builder.HasIndex(pq => pq.PersonalityTestId);
        builder.HasIndex(pq => new { pq.PersonalityTestId, pq.OrderIndex });

        // Relationships
        builder.HasOne(pq => pq.PersonalityTest)
            .WithMany(pt => pt.Questions)
            .HasForeignKey(pq => pq.PersonalityTestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
