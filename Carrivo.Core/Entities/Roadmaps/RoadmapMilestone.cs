namespace Carrivo.Core.Entities;

public class RoadmapMilestone : BaseEntity
{
    public Guid RoadmapId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int? EstimatedDurationDays { get; set; }
    
    // Navigation Properties
    public Roadmap Roadmap { get; set; } = null!;
    public ICollection<MilestoneTask> Tasks { get; set; } = new List<MilestoneTask>();
}
