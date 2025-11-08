namespace Carrivo.Core.Entities;

public class Track : BaseEntity
{
    public string Code { get; set; } = string.Empty; // e.g., "BACKEND", "FRONTEND"
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation Properties
    public ICollection<PersonalityTypeTrack> PersonalityTypeTracks { get; set; } = new List<PersonalityTypeTrack>();
    public ICollection<MentorTrack> MentorTracks { get; set; } = new List<MentorTrack>();
    public ICollection<Roadmap> Roadmaps { get; set; } = new List<Roadmap>();
    public ICollection<TestRecommendation> TestRecommendations { get; set; } = new List<TestRecommendation>();
}
