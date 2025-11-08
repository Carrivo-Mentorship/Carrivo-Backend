namespace Carrivo.Core.Entities;

public class PersonalityTest : BaseEntity
{
    public string Version { get; set; } = "1.0";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Navigation Properties
    public ICollection<PersonalityQuestion> Questions { get; set; } = new List<PersonalityQuestion>();
    public ICollection<StudentTestAttempt> TestAttempts { get; set; } = new List<StudentTestAttempt>();
}
