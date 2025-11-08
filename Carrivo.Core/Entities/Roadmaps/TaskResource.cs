using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class TaskResource : BaseEntity
{
    public Guid MilestoneTaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ResourceType ResourceType { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsFree { get; set; } = true;
    public int OrderIndex { get; set; }
    
    // Navigation Properties
    public MilestoneTask MilestoneTask { get; set; } = null!;
}
