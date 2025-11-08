namespace Carrivo.Core.Entities;

public class PersonalityType : BaseEntity
{
    public string Code { get; set; } = string.Empty; // e.g., "ANALYTICAL", "CREATIVE"
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true; // Soft Delete
    
    // Navigation Properties
    public ICollection<StudentTestAttempt> StudentTestAttempts { get; set; } = new List<StudentTestAttempt>();
    public ICollection<PersonalityTypeTrack> PersonalityTypeTracks { get; set; } = new List<PersonalityTypeTrack>();
}
