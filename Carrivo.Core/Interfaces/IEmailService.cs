namespace Carrivo.Core.Interfaces;

public interface IEmailService
{
    Task SendVerificationOtpAsync(string toEmail, string userName, string otpCode);
    Task SendPasswordResetOtpAsync(string toEmail, string userName, string otpCode);
    Task SendPasswordChangedNotificationAsync(string toEmail, string userName);
    Task SendWelcomeEmailAsync(string toEmail, string userName);
}
