using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MentorProfile : BaseEntity
{
    // Foreign Key
    public Guid UserId { get; set; }
    
    // Professional Data
    public string JobTitle { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string ProfessionalBio { get; set; } = string.Empty;
    
    // Mentorship Data
    public string? AvailabilitySchedule { get; set; }
    public decimal? PricePerHour { get; set; }
    public string? SpokenLanguages { get; set; }
    
    // Rating
    public decimal AverageRating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    
    // Navigation Properties
    public Users User { get; set; } = null!;
    public ICollection<MentorTrack> MentorTracks { get; set; } = new List<MentorTrack>();
    public ICollection<MentorTrackLevel> MentorTrackLevels { get; set; } = new List<MentorTrackLevel>();
    public ICollection<MentorshipRequest> MentorshipRequests { get; set; } = new List<MentorshipRequest>();
    public ICollection<MentorshipSession> MentorshipSessions { get; set; } = new List<MentorshipSession>();
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}
