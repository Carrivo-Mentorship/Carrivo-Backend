using System.ComponentModel.DataAnnotations;

namespace Carrivo.Core.Entities;

public class EmailVerification
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required, StringLength(6, MinimumLength = 6)]
    public string OtpCode { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    
    public bool IsUsed { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public int AttemptCount { get; set; } = 0;
    
    [MaxLength(50)]
    public string? RequestedFromIp { get; set; }
    
    [Required, MaxLength(50)]
    public string VerificationType { get; set; } = string.Empty; // "EmailVerification" or "PasswordReset"
    
    // Navigation Property
    public virtual Users User { get; set; } = null!;
    
    // Computed Property
    public bool IsValid => !IsUsed && DateTime.UtcNow < ExpiresAt && AttemptCount < 5;
}
