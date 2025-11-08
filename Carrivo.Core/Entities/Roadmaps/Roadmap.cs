using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class Roadmap : BaseEntity
{
    public Guid TrackId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TrackLevel Level { get; set; }
    public RoadmapCreatorType CreatorType { get; set; }
    public int? EstimatedDurationDays { get; set; }
    
    // Navigation Properties
    public Track Track { get; set; } = null!;
    public ICollection<RoadmapMilestone> Milestones { get; set; } = new List<RoadmapMilestone>();
    public ICollection<StudentRoadmap> StudentRoadmaps { get; set; } = new List<StudentRoadmap>();
}
