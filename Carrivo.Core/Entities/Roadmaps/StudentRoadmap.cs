namespace Carrivo.Core.Entities;

// Student's customized roadmap instance
public class StudentRoadmap : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid RoadmapId { get; set; }
    public Guid? CreatedByMentorId { get; set; } // If created by mentor
    public DateTime StartDate { get; set; }
    public DateTime? TargetEndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal ProgressPercentage { get; set; } = 0;
    
    // Customization based on student data
    public decimal? DailyStudyHours { get; set; }
    public string? CustomizationNotes { get; set; }
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public Roadmap Roadmap { get; set; } = null!;
    public MentorProfile? CreatedByMentor { get; set; }
}
