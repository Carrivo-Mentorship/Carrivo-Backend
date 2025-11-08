namespace Carrivo.Application.DTOs.Auth.Responses;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public UserInfoDto User { get; set; } = null!;
}
