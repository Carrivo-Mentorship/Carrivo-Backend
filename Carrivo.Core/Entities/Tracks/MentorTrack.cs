namespace Carrivo.Core.Entities;

// Many-to-Many: Mentor specializes in Tracks
public class MentorTrack : BaseEntity
{
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    
    // Navigation Properties
    public MentorProfile MentorProfile { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
