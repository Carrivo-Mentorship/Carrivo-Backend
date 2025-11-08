namespace Carrivo.Core.Entities;

public class StudentTestAttempt : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid PersonalityTestId { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; } = false;
    
    // ML Model Output
    public Guid? PersonalityTypeId { get; set; }
    public string? MlModelOutput { get; set; } // JSON with scores and recommendations
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public PersonalityTest PersonalityTest { get; set; } = null!;
    public PersonalityType? PersonalityType { get; set; }
    public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    public ICollection<TestRecommendation> TestRecommendations { get; set; } = new List<TestRecommendation>();
}
