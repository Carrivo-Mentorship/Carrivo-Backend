using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
{
    public void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        builder.HasKey(sa => sa.Id);

        // Properties
        builder.Property(sa => sa.AnswerValue)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(sa => sa.StudentTestAttemptId);
        builder.HasIndex(sa => sa.PersonalityQuestionId);
        builder.HasIndex(sa => new { sa.StudentTestAttemptId, sa.PersonalityQuestionId })
            .IsUnique();

        // Relationships
        builder.HasOne(sa => sa.StudentTestAttempt)
            .WithMany(sta => sta.StudentAnswers)
            .HasForeignKey(sa => sa.StudentTestAttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sa => sa.PersonalityQuestion)
            .WithMany(pq => pq.StudentAnswers)
            .HasForeignKey(sa => sa.PersonalityQuestionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
