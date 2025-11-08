using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class StudentProfile : BaseEntity
{
    // Foreign Key
    public Guid UserId { get; set; }
    
    // Academic & Professional Data
    public string? Education { get; set; }
    public EmploymentStatus? EmploymentStatus { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Experience { get; set; }
    public string? CvUrl { get; set; }
    
    // Learning Related Data
    public decimal? DailyStudyHours { get; set; }
    public string? CurrentSkills { get; set; }
    public string? PreferredFields { get; set; }
    public LearningStyle? PreferredLearningStyle { get; set; }
    
    // Navigation Properties
    public Users User { get; set; } = null!;
    public ICollection<StudentTestAttempt> TestAttempts { get; set; } = new List<StudentTestAttempt>();
    public ICollection<StudentRoadmap> StudentRoadmaps { get; set; } = new List<StudentRoadmap>();
    public ICollection<StudentProgress> StudentProgress { get; set; } = new List<StudentProgress>();
    public ICollection<MentorshipRequest> MentorshipRequests { get; set; } = new List<MentorshipRequest>();
    public ICollection<MentorshipSession> MentorshipSessions { get; set; } = new List<MentorshipSession>();
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}
