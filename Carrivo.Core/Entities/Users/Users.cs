using Carrivo.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Carrivo.Core.Entities;

public class Users : IdentityUser<Guid>
{
    // Basic Info
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    // User Type
    public UserType UserType { get; set; }
    
    // Authentication Fields
    public DateTime? EmailVerifiedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? LastLoginIp { get; set; }
    
    // OAuth Fields (for future Google/Facebook login)
    public string? OAuthProvider { get; set; }
    public string? OAuthProviderId { get; set; }
    public bool IsOAuthAccount { get; set; } = false;
    
    // Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    
    // Navigation Properties
    public StudentProfile? StudentProfile { get; set; }
    public MentorProfile? MentorProfile { get; set; }
    public ICollection<EmailVerification> EmailVerifications { get; set; } = new List<EmailVerification>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
