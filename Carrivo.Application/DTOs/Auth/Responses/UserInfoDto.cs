using Carrivo.Core.Enums;

namespace Carrivo.Application.DTOs.Auth.Responses;

public class UserInfoDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public bool EmailVerified { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public Dictionary<string, object>? AdditionalInfo { get; set; }
}
