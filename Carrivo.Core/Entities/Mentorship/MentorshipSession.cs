using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MentorshipSession : BaseEntity
{
    public Guid MentorshipRequestId { get; set; }
    public Guid StudentProfileId { get; set; }
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public MentorshipSessionStatus Status { get; set; } = MentorshipSessionStatus.PendingPayment;
    
    // Payment Info
    public bool IsPaid { get; set; } = false;
    public decimal? PaidAmount { get; set; }
    public DateTime? PaidAt { get; set; }
    
    // Rating & Review (after completion)
    public int? Rating { get; set; }
    public string? Review { get; set; }
    public DateTime? ReviewedAt { get; set; }
    
    // Navigation Properties
    public MentorshipRequest MentorshipRequest { get; set; } = null!;
    public StudentProfile StudentProfile { get; set; } = null!;
    public MentorProfile MentorProfile { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
