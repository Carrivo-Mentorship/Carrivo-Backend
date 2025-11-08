namespace Carrivo.Core.Entities;

public class StudentProgress : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid MilestoneTaskId { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedAt { get; set; }
    public decimal? TimeSpentHours { get; set; }
    public string? Notes { get; set; }
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public MilestoneTask MilestoneTask { get; set; } = null!;
}
