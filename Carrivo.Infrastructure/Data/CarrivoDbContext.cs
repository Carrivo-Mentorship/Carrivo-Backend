using Carrivo.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Carrivo.Infrastructure.Data;

public class CarrivoDbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid>
{
    public CarrivoDbContext(DbContextOptions<CarrivoDbContext> options) 
        : base(options)
    {
    }

    // User Profiles
    public DbSet<StudentProfile> StudentProfiles { get; set; }
    public DbSet<MentorProfile> MentorProfiles { get; set; }

    // Authentication
    public DbSet<EmailVerification> EmailVerifications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // Personality Test
    public DbSet<PersonalityType> PersonalityTypes { get; set; }
    public DbSet<PersonalityTest> PersonalityTests { get; set; }
    public DbSet<PersonalityQuestion> PersonalityQuestions { get; set; }
    public DbSet<StudentTestAttempt> StudentTestAttempts { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }

    // Tracks & Recommendations
    public DbSet<Track> Tracks { get; set; }
    public DbSet<PersonalityTypeTrack> PersonalityTypeTracks { get; set; }
    public DbSet<TestRecommendation> TestRecommendations { get; set; }
    public DbSet<MentorTrack> MentorTracks { get; set; }
    public DbSet<MentorTrackLevel> MentorTrackLevels { get; set; }

    // Roadmaps
    public DbSet<Roadmap> Roadmaps { get; set; }
    public DbSet<StudentRoadmap> StudentRoadmaps { get; set; }
    public DbSet<RoadmapMilestone> RoadmapMilestones { get; set; }
    public DbSet<MilestoneTask> MilestoneTasks { get; set; }
    public DbSet<TaskResource> TaskResources { get; set; }
    public DbSet<StudentProgress> StudentProgress { get; set; }

    // Mentorship
    public DbSet<MentorshipRequest> MentorshipRequests { get; set; }
    public DbSet<MentorshipSession> MentorshipSessions { get; set; }

    // Chat
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageAttachment> MessageAttachments { get; set; }

    // Notifications
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarrivoDbContext).Assembly);
    }
}
