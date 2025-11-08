using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MessageAttachment : BaseEntity
{
    public Guid MessageId { get; set; }
    public MessageAttachmentType AttachmentType { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public long? FileSizeBytes { get; set; }
    
    // Navigation Properties
    public Message Message { get; set; } = null!;
}
