using Carrivo.Application.DTOs.Auth.Requests;
using Carrivo.Application.DTOs.Auth.Responses;
using Carrivo.Application.DTOs.Common;
using Carrivo.Application.Interfaces;
using Carrivo.Application.Settings;
using Carrivo.Core.Constants;
using Carrivo.Core.Entities;
using Carrivo.Core.Enums;
using Carrivo.Core.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Carrivo.Application.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;
    private readonly CarrivoDbContext _context;
    private readonly IOtpService _otpService;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(
        UserManager<Users> userManager,
        SignInManager<Users> signInManager,
        CarrivoDbContext context,
        IOtpService otpService,
        ITokenService tokenService,
        IEmailService emailService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _otpService = otpService;
        _tokenService = tokenService;
        _emailService = emailService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequest request, string? ipAddress = null)
    {
        // Check if email exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return ApiResponse<AuthResponseDto>.Failure("Email already registered");

        // Create user entity
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            UserType = request.UserType,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };

        // Create user with hashed password
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return ApiResponse<AuthResponseDto>.Failure("Registration failed", errors);
        }

        // Create profile based on user type
        await CreateUserProfileAsync(user.Id, request.UserType);

        // Generate OTP
        var otpCode = await _otpService.GenerateAndStoreOtpAsync(
            user.Id,
            user.Email,
            VerificationTypes.EmailVerification,
            ipAddress);

        // Send verification email
        try
        {
            await _emailService.SendVerificationOtpAsync(user.Email, user.FirstName, otpCode);
        }
        catch (Exception)
        {
            // Log error but don't fail registration
        }

        // Generate tokens
        var authResponse = await GenerateAuthResponseAsync(user, ipAddress);

        return ApiResponse<AuthResponseDto>.Success(
            authResponse,
            "Registration successful! Please check your email to verify your account.");
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequest request, string? ipAddress = null)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return ApiResponse<AuthResponseDto>.Failure("Invalid credentials", statusCode: 401);

        // Check if account is deleted
        if (user.IsDeleted)
            return ApiResponse<AuthResponseDto>.Failure("Account deactivated", statusCode: 403);

        // Check if account is locked
        if (await _userManager.IsLockedOutAsync(user))
        {
            var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
            return ApiResponse<AuthResponseDto>.Failure(
                $"Account locked. Try again after {lockoutEnd?.ToString("yyyy-MM-dd HH:mm")}",
                statusCode: 403);
        }

        // Verify password
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                return ApiResponse<AuthResponseDto>.Failure("Too many failed attempts. Account locked for 15 minutes.", statusCode: 403);

            return ApiResponse<AuthResponseDto>.Failure("Invalid credentials", statusCode: 401);
        }

        // Update login tracking
        user.LastLoginAt = DateTime.UtcNow;
        user.LastLoginIp = ipAddress;
        await _userManager.UpdateAsync(user);

        // Generate tokens
        var authResponse = await GenerateAuthResponseAsync(user, ipAddress);

        return ApiResponse<AuthResponseDto>.Success(authResponse, "Login successful");
    }

    public async Task<ApiResponse<bool>> VerifyEmailAsync(VerifyOtpRequest request)
    {
        // Validate OTP
        var isValidOtp = await _otpService.ValidateOtpAsync(
            request.Email,
            request.OtpCode,
            VerificationTypes.EmailVerification);

        if (!isValidOtp)
            return ApiResponse<bool>.Failure("Invalid or expired OTP code");

        // Find user
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return ApiResponse<bool>.Failure("User not found");

        // Mark email as verified
        user.EmailConfirmed = true;
        user.EmailVerifiedAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        // Send welcome email
        try
        {
            await _emailService.SendWelcomeEmailAsync(user.Email, user.FirstName);
        }
        catch (Exception)
        {
            // Log error but don't fail verification
        }

        return ApiResponse<bool>.Success(true, "Email verified successfully!");
    }

    public async Task<ApiResponse<bool>> ResendVerificationOtpAsync(ResendOtpRequest request, string? ipAddress = null)
    {
        // Find user
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return ApiResponse<bool>.Success(true, "If your email exists, you'll receive a verification code");

        // Check if already verified
        if (user.EmailConfirmed)
            return ApiResponse<bool>.Failure("Email already verified");

        // Check rate limiting
        var canResend = await _otpService.CanResendOtpAsync(request.Email);
        if (!canResend)
            return ApiResponse<bool>.Failure("Too many requests. Please try again later.", statusCode: 429);

        // Generate new OTP
        var otpCode = await _otpService.GenerateAndStoreOtpAsync(
            user.Id,
            user.Email,
            VerificationTypes.EmailVerification,
            ipAddress);

        // Send email
        try
        {
            await _emailService.SendVerificationOtpAsync(user.Email, user.FirstName, otpCode);
        }
        catch (Exception)
        {
            return ApiResponse<bool>.Failure("Failed to send email. Please try again later.");
        }

        return ApiResponse<bool>.Success(true, "Verification code sent to your email");
    }

    public async Task<ApiResponse<bool>> ForgotPasswordAsync(ForgotPasswordRequest request, string? ipAddress = null)
    {
        // Find user (don't reveal if exists)
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return ApiResponse<bool>.Success(true,
                "If your email exists, you'll receive a password reset code");
        }

        // Check if OAuth account
        if (user.IsOAuthAccount)
        {
            return ApiResponse<bool>.Failure("OAuth accounts cannot reset password");
        }

        // Check rate limiting
        var canSend = await _otpService.CanResendOtpAsync(request.Email);
        if (!canSend)
        {
            return ApiResponse<bool>.Failure("Too many requests. Please try again later.", statusCode: 429);
        }

        // Generate password reset OTP
        var otpCode = await _otpService.GenerateAndStoreOtpAsync(
            user.Id,
            user.Email,
            VerificationTypes.PasswordReset,
            ipAddress);

        // Send reset email
        try
        {
            await _emailService.SendPasswordResetOtpAsync(user.Email, user.FirstName, otpCode);
        }
        catch (Exception)
        {
            return ApiResponse<bool>.Failure("Failed to send email. Please try again later.");
        }

        return ApiResponse<bool>.Success(true, "Password reset code sent to your email");
    }

    public async Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        // Validate OTP
        var isValidOtp = await _otpService.ValidateOtpAsync(
            request.Email,
            request.OtpCode,
            VerificationTypes.PasswordReset);

        if (!isValidOtp)
        {
            return ApiResponse<bool>.Failure("Invalid or expired OTP code");
        }

        // Find user
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return ApiResponse<bool>.Failure("User not found");
        }

        // Generate password reset token
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Reset password
        var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return ApiResponse<bool>.Failure("Password reset failed", errors);
        }

        // Clear lockout (if any)
        await _userManager.SetLockoutEndDateAsync(user, null);
        await _userManager.ResetAccessFailedCountAsync(user);

        // Revoke all refresh tokens (security measure)
        await _tokenService.RevokeAllUserRefreshTokensAsync(user.Id, "Password reset");

        // Send confirmation email
        try
        {
            await _emailService.SendPasswordChangedNotificationAsync(user.Email, user.FirstName);
        }
        catch (Exception)
        {
            // Log error but don't fail reset
        }

        return ApiResponse<bool>.Success(true, "Password reset successful! Please login with your new password.");
    }

    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequest request, string? ipAddress = null)
    {
        // Validate refresh token
        var userId = await _tokenService.ValidateRefreshTokenAsync(request.RefreshToken);
        if (userId == null)
            return ApiResponse<AuthResponseDto>.Failure("Invalid or expired refresh token", statusCode: 401);

        // Find user
        var user = await _userManager.FindByIdAsync(userId.ToString()!);
        if (user == null || user.IsDeleted)
            return ApiResponse<AuthResponseDto>.Failure("User not found", statusCode: 404);

        // Revoke old refresh token
        await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken, "Replaced by new token", ipAddress);

        // Generate new tokens
        var authResponse = await GenerateAuthResponseAsync(user, ipAddress);

        return ApiResponse<AuthResponseDto>.Success(authResponse, "Token refreshed successfully");
    }

    public async Task<ApiResponse<bool>> LogoutAsync(string refreshToken, string? ipAddress = null)
    {
        await _tokenService.RevokeRefreshTokenAsync(refreshToken, "User logout", ipAddress);
        return ApiResponse<bool>.Success(true, "Logged out successfully");
    }

    // Helper Methods

    private async Task<AuthResponseDto> GenerateAuthResponseAsync(Users user, string? ipAddress)
    {
        // Generate Access Token
        var additionalClaims = new Dictionary<string, string>
        {
            ["firstName"] = user.FirstName,
            ["lastName"] = user.LastName,
            ["emailVerified"] = user.EmailConfirmed.ToString()
        };

        var accessToken = _tokenService.GenerateAccessToken(
            user.Id,
            user.Email!,
            user.UserType.ToString(),
            additionalClaims);

        // Generate Refresh Token
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Store Refresh Token in Database
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            CreatedByIp = ipAddress
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        // Clean up old refresh tokens (keep last 5)
        await CleanupOldRefreshTokensAsync(user.Id);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            TokenType = "Bearer",
            User = new UserInfoDto
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                EmailVerified = user.EmailConfirmed,
                ProfilePictureUrl = user.ProfilePictureUrl,
                AdditionalInfo = await GetUserAdditionalInfoAsync(user)
            }
        };
    }

    private async Task CleanupOldRefreshTokensAsync(Guid userId)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .OrderByDescending(rt => rt.CreatedAt)
            .Skip(5)
            .ToListAsync();

        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }

    private async Task CreateUserProfileAsync(Guid userId, UserType userType)
    {
        if (userType == UserType.Student)
        {
            var studentProfile = new StudentProfile
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };
            _context.StudentProfiles.Add(studentProfile);
        }
        else if (userType == UserType.Mentor)
        {
            var mentorProfile = new MentorProfile
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                JobTitle = string.Empty,
                ProfessionalBio = string.Empty,
                CreatedAt = DateTime.UtcNow
            };
            _context.MentorProfiles.Add(mentorProfile);
        }

        await _context.SaveChangesAsync();
    }

    private async Task<Dictionary<string, object>> GetUserAdditionalInfoAsync(Users user)
    {
        var info = new Dictionary<string, object>
        {
            ["hasProfile"] = false,
            ["profileComplete"] = false
        };

        if (user.UserType == UserType.Student)
        {
            var profile = await _context.StudentProfiles
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (profile != null)
            {
                info["hasProfile"] = true;
                info["profileComplete"] = !string.IsNullOrEmpty(profile.Education);
            }
        }
        else if (user.UserType == UserType.Mentor)
        {
            var profile = await _context.MentorProfiles
                .FirstOrDefaultAsync(mp => mp.UserId == user.Id);

            if (profile != null)
            {
                info["hasProfile"] = true;
                info["profileComplete"] = !string.IsNullOrEmpty(profile.JobTitle) &&
                                         !string.IsNullOrEmpty(profile.ProfessionalBio);
            }
        }

        return info;
    }
}
