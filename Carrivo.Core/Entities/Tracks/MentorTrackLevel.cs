using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

// Mentor can teach specific levels
public class MentorTrackLevel : BaseEntity
{
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    public TrackLevel Level { get; set; }
    
    // Navigation Properties
    public MentorProfile MentorProfile { get; set; } = null!;
}
