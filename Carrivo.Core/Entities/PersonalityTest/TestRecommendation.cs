namespace Carrivo.Core.Entities;

// Top 3 recommended tracks for a test attempt
public class TestRecommendation : BaseEntity
{
    public Guid StudentTestAttemptId { get; set; }
    public Guid TrackId { get; set; }
    public decimal Score { get; set; } // ML model score
    public int Rank { get; set; } // 1, 2, 3
    
    // Navigation Properties
    public StudentTestAttempt StudentTestAttempt { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
