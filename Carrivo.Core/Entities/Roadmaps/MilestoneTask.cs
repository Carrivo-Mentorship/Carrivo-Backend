namespace Carrivo.Core.Entities;

public class MilestoneTask : BaseEntity
{
    public Guid RoadmapMilestoneId { get; set; }
    public string Title { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int? EstimatedHours { get; set; }
    public bool IsOptional { get; set; } = false;
    
    // Navigation Properties
    public RoadmapMilestone RoadmapMilestone { get; set; } = null!;
    public ICollection<TaskResource> Resources { get; set; } = new List<TaskResource>();
    public ICollection<StudentProgress> StudentProgress { get; set; } = new List<StudentProgress>();
}
