namespace Carrivo.Core.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, string userType, Dictionary<string, string>? additionalClaims = null);
    string GenerateRefreshToken();
    Task<Guid?> ValidateRefreshTokenAsync(string refreshToken);
    Task RevokeRefreshTokenAsync(string refreshToken, string? reason = null, string? ipAddress = null);
    Task RevokeAllUserRefreshTokensAsync(Guid userId, string? reason = null);
    Task CleanupExpiredTokensAsync();
}
