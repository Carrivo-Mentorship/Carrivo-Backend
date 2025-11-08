using Carrivo.Application.Settings;
using Carrivo.Core.Constants;
using Carrivo.Core.Entities;
using Carrivo.Core.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Carrivo.Application.Services.Auth;

public class OtpService : IOtpService
{
    private readonly CarrivoDbContext _context;
    private readonly EmailSettings _emailSettings;

    public OtpService(CarrivoDbContext context, IOptions<EmailSettings> emailSettings)
    {
        _context = context;
        _emailSettings = emailSettings.Value;
    }

    public async Task<string> GenerateAndStoreOtpAsync(Guid userId, string email, string verificationType, string? ipAddress = null)
    {
        // Generate secure 6-digit OTP
        var otpCode = GenerateSecureOtp(_emailSettings.OtpLength);

        // Set expiration based on type
        var expirationMinutes = verificationType == VerificationTypes.PasswordReset
            ? _emailSettings.PasswordResetOtpExpirationMinutes
            : _emailSettings.VerificationOtpExpirationMinutes;

        // Invalidate all previous OTPs of this type
        await InvalidateAllOtpsAsync(userId, verificationType);

        // Create new OTP record
        var verification = new EmailVerification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Email = email,
            OtpCode = otpCode,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
            VerificationType = verificationType,
            RequestedFromIp = ipAddress,
            IsUsed = false,
            AttemptCount = 0
        };

        _context.EmailVerifications.Add(verification);
        await _context.SaveChangesAsync();

        return otpCode;
    }

    public async Task<bool> ValidateOtpAsync(string email, string otpCode, string verificationType)
    {
        // Find latest valid OTP
        var verification = await _context.EmailVerifications
            .Where(v => v.Email == email
                && v.VerificationType == verificationType
                && !v.IsUsed
                && v.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(v => v.CreatedAt)
            .FirstOrDefaultAsync();

        if (verification == null)
            return false;

        // Increment attempt count
        verification.AttemptCount++;

        // Check max attempts (5)
        if (verification.AttemptCount > 5)
        {
            verification.IsUsed = true;
            await _context.SaveChangesAsync();
            return false;
        }

        // Validate OTP code
        if (verification.OtpCode != otpCode)
        {
            await _context.SaveChangesAsync();
            return false;
        }

        // Mark as used
        verification.IsUsed = true;
        verification.VerifiedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CanResendOtpAsync(string email)
    {
        var oneHourAgo = DateTime.UtcNow.AddHours(-1);

        var recentOtpCount = await _context.EmailVerifications
            .Where(v => v.Email == email && v.CreatedAt >= oneHourAgo)
            .CountAsync();

        return recentOtpCount < _emailSettings.MaxOtpResendAttemptsPerHour;
    }

    public async Task InvalidateAllOtpsAsync(Guid userId, string verificationType)
    {
        var existingOtps = await _context.EmailVerifications
            .Where(v => v.UserId == userId
                && v.VerificationType == verificationType
                && !v.IsUsed)
            .ToListAsync();

        foreach (var otp in existingOtps)
        {
            otp.IsUsed = true;
        }

        await _context.SaveChangesAsync();
    }

    private string GenerateSecureOtp(int length)
    {
        const string digits = "0123456789";
        var otp = new char[length];

        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[length];
        rng.GetBytes(randomBytes);

        for (int i = 0; i < length; i++)
        {
            otp[i] = digits[randomBytes[i] % digits.Length];
        }

        return new string(otp);
    }
}
