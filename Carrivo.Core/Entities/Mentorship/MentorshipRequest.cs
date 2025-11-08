using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MentorshipRequest : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    public string Message { get; set; } = string.Empty;
    public MentorshipRequestStatus Status { get; set; } = MentorshipRequestStatus.Pending;
    public DateTime? RespondedAt { get; set; }
    public string? ResponseMessage { get; set; }
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public MentorProfile MentorProfile { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public MentorshipSession? MentorshipSession { get; set; }
}
