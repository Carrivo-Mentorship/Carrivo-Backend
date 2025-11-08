namespace Carrivo.Core.Entities;

public class StudentAnswer : BaseEntity
{
    public Guid StudentTestAttemptId { get; set; }
    public Guid PersonalityQuestionId { get; set; }
    public string AnswerValue { get; set; } = string.Empty; // Could be text, number, or JSON
    
    // Navigation Properties
    public StudentTestAttempt StudentTestAttempt { get; set; } = null!;
    public PersonalityQuestion PersonalityQuestion { get; set; } = null!;
}
