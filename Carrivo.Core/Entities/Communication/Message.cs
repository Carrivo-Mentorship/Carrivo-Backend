namespace Carrivo.Core.Entities;

public class Message : BaseEntity
{
    public Guid MentorshipSessionId { get; set; }
    public Guid? SenderStudentId { get; set; }
    public Guid? SenderMentorId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    
    // Navigation Properties
    public MentorshipSession MentorshipSession { get; set; } = null!;
    public StudentProfile? SenderStudent { get; set; }
    public MentorProfile? SenderMentor { get; set; }
    public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
}
