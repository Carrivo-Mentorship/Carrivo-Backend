namespace Carrivo.Core.Entities;

public class PersonalityTypeTrack : BaseEntity
{
    public Guid PersonalityTypeId { get; set; }
    public Guid TrackId { get; set; }
    public decimal CompatibilityScore { get; set; } // 0-100
    
    // Navigation Properties
    public PersonalityType PersonalityType { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
