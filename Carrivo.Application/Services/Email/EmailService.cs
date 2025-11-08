using Carrivo.Application.Settings;
using Carrivo.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Carrivo.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendVerificationOtpAsync(string toEmail, string userName, string otpCode)
    {
        var subject = "Verify Your Email - Carrivo";
        var body = GetVerificationEmailTemplate(userName, otpCode);
        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendPasswordResetOtpAsync(string toEmail, string userName, string otpCode)
    {
        var subject = "Password Reset Request - Carrivo";
        var body = GetPasswordResetEmailTemplate(userName, otpCode);
        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendPasswordChangedNotificationAsync(string toEmail, string userName)
    {
        var subject = "Password Changed Successfully - Carrivo";
        var body = GetPasswordChangedEmailTemplate(userName);
        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendWelcomeEmailAsync(string toEmail, string userName)
    {
        var subject = "Welcome to Carrivo!";
        var body = GetWelcomeEmailTemplate(userName);
        await SendEmailAsync(toEmail, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            _logger.LogInformation("Attempting to send email to {ToEmail} using SMTP {SmtpHost}:{SmtpPort}", 
                toEmail, _emailSettings.SmtpHost, _emailSettings.SmtpPort);
            
            using var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                EnableSsl = _emailSettings.EnableSsl,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
            
            _logger.LogInformation("Email sent successfully to {ToEmail}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ToEmail}. Error: {ErrorMessage}", toEmail, ex.Message);
            throw new Exception($"Failed to send email: {ex.Message}", ex);
        }
    }

    private string GetVerificationEmailTemplate(string userName, string otpCode)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .otp-code {{ 
            font-size: 36px; 
            font-weight: bold; 
            color: #667eea; 
            letter-spacing: 8px; 
            text-align: center; 
            padding: 25px; 
            background-color: white; 
            border-radius: 10px; 
            margin: 25px 0;
            border: 2px dashed #667eea;
        }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎓 Carrivo</h1>
            <p>Email Verification</p>
        </div>
        <div class='content'>
            <h2>Hello {userName},</h2>
            <p>Thank you for registering with Carrivo! Please use the following OTP code to verify your email:</p>
            
            <div class='otp-code'>{otpCode}</div>
            
            <p>This code will expire in <strong>10 minutes</strong>.</p>
            <p>If you didn't create an account, please ignore this email.</p>
        </div>
        <div class='footer'>
            <p>&copy; 2024 Carrivo. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string GetPasswordResetEmailTemplate(string userName, string otpCode)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .otp-code {{ 
            font-size: 36px; 
            font-weight: bold; 
            color: #f5576c; 
            letter-spacing: 8px; 
            text-align: center; 
            padding: 25px; 
            background-color: white; 
            border-radius: 10px; 
            margin: 25px 0;
            border: 2px dashed #f5576c;
        }}
        .warning {{ background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎓 Carrivo</h1>
            <p>Password Reset Request</p>
        </div>
        <div class='content'>
            <h2>Hello {userName},</h2>
            <p>We received a request to reset your password. Use the following OTP code:</p>
            
            <div class='otp-code'>{otpCode}</div>
            
            <p>This code will expire in <strong>15 minutes</strong>.</p>
            
            <div class='warning'>
                <strong>⚠️ Security Alert:</strong> 
                If you didn't request a password reset, please ignore this email and ensure your account is secure.
            </div>
        </div>
        <div class='footer'>
            <p>&copy; 2024 Carrivo. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string GetPasswordChangedEmailTemplate(string userName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .success-icon {{ font-size: 60px; text-align: center; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎓 Carrivo</h1>
            <p>Password Changed</p>
        </div>
        <div class='content'>
            <div class='success-icon'>✅</div>
            <h2>Hello {userName},</h2>
            <p>Your password has been successfully changed.</p>
            <p>If you didn't make this change, please contact our support team immediately.</p>
            <p>Login with your new password to continue using Carrivo.</p>
        </div>
        <div class='footer'>
            <p>&copy; 2024 Carrivo. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string GetWelcomeEmailTemplate(string userName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .welcome-icon {{ font-size: 60px; text-align: center; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎓 Carrivo</h1>
            <p>Welcome Aboard!</p>
        </div>
        <div class='content'>
            <div class='welcome-icon'>🎉</div>
            <h2>Welcome {userName}!</h2>
            <p>Your email has been verified successfully. You're all set to start your learning journey with Carrivo!</p>
            <p>Explore personalized roadmaps, connect with mentors, and achieve your career goals.</p>
            <p>Happy learning!</p>
        </div>
        <div class='footer'>
            <p>&copy; 2024 Carrivo. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
    }
}
