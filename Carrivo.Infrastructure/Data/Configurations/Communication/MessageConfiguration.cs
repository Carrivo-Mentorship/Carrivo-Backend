using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Communication;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        // Properties
        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(5000);

        // Indexes
        builder.HasIndex(m => m.MentorshipSessionId);
        builder.HasIndex(m => m.SenderStudentId);
        builder.HasIndex(m => m.SenderMentorId);
        builder.HasIndex(m => new { m.MentorshipSessionId, m.CreatedAt });
        builder.HasIndex(m => m.IsRead);

        // Relationships
        builder.HasOne(m => m.MentorshipSession)
            .WithMany(ms => ms.Messages)
            .HasForeignKey(m => m.MentorshipSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.SenderStudent)
            .WithMany(sp => sp.SentMessages)
            .HasForeignKey(m => m.SenderStudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.SenderMentor)
            .WithMany(mp => mp.SentMessages)
            .HasForeignKey(m => m.SenderMentorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
