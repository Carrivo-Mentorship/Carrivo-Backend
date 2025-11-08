using System.ComponentModel.DataAnnotations;

namespace Carrivo.Core.Entities;

public class RefreshToken
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Token { get; set; } = string.Empty; // Base64 encoded
    
    public Guid UserId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    
    public bool IsRevoked { get; set; } = false;
    public DateTime? RevokedAt { get; set; }
    
    [MaxLength(50)]
    public string? CreatedByIp { get; set; }
    
    [MaxLength(50)]
    public string? RevokedByIp { get; set; }
    
    [MaxLength(200)]
    public string? RevocationReason { get; set; }
    
    // Navigation Property
    public virtual Users User { get; set; } = null!;
    
    // Computed Properties
    public bool IsActive => !IsRevoked && DateTime.UtcNow < ExpiresAt;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
}
