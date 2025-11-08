namespace Carrivo.Core.Interfaces;

public interface IOtpService
{
    Task<string> GenerateAndStoreOtpAsync(Guid userId, string email, string verificationType, string? ipAddress = null);
    Task<bool> ValidateOtpAsync(string email, string otpCode, string verificationType);
    Task<bool> CanResendOtpAsync(string email);
    Task InvalidateAllOtpsAsync(Guid userId, string verificationType);
}
