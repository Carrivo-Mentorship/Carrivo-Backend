namespace Carrivo.Application.Settings;

public class EmailSettings
{
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
    
    // OTP Settings
    public int OtpLength { get; set; } = 6;
    public int VerificationOtpExpirationMinutes { get; set; } = 10;
    public int PasswordResetOtpExpirationMinutes { get; set; } = 15;
    public int MaxOtpResendAttemptsPerHour { get; set; } = 5;
}
