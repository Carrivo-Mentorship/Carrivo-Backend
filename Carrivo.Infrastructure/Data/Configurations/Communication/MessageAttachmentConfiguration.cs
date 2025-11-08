using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Communication;

public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
{
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.HasKey(ma => ma.Id);

        // Properties
        builder.Property(ma => ma.Url)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(ma => ma.FileName)
            .HasMaxLength(255);

        // Indexes
        builder.HasIndex(ma => ma.MessageId);
        builder.HasIndex(ma => ma.AttachmentType);

        // Relationships
        builder.HasOne(ma => ma.Message)
            .WithMany(m => m.Attachments)
            .HasForeignKey(ma => ma.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
