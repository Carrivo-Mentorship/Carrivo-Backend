# ForgotPasswordRequest.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
}
```

# LoginRequest.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
```

# RefreshTokenRequest.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Refresh token is required")]
    public string RefreshToken { get; set; } = string.Empty;
}
```

# RegisterRequest.cs
```cs
using Carrivo.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class RegisterRequest
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "User type is required")]
    public UserType UserType { get; set; }
}
```

# ResendOtpRequest.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class ResendOtpRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
}
```

# ResetPasswordRequest.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class ResetPasswordRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "OTP code is required")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be 6 digits")]
    public string OtpCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "New password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
```

# VerifyOtpRequest.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class VerifyOtpRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "OTP code is required")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be 6 digits")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP must be 6 digits")]
    public string OtpCode { get; set; } = string.Empty;
}
```

# AuthResponseDto.cs
```cs
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
```

# UserInfoDto.cs
```cs
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
```

# ApiResponse.cs
```cs
namespace Carrivo.Application.DTOs.Common;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string>? Errors { get; set; }
    public int StatusCode { get; set; }

    public static ApiResponse<T> Success(T data, string message = "Operation successful", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static ApiResponse<T> Failure(string message, IEnumerable<string>? errors = null, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}
```

# MentorCardDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Mentor_System
{
    public class MentorCardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public string ExperienceLevel { get; set; } = string.Empty; // junior, mid, senior
        public string Availability { get; set; } = string.Empty; // online, offline, busy
        public string Track { get; set; } = string.Empty;
    }
}
```

# MentorDetailDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Mentor_System
{


    // Response للـ GET /api/mentors/:id
    public class MentorDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public List<ReviewDto> Reviews { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public string ExperienceSummary { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public decimal Rating { get; set; }
        public string? Image { get; set; }
    }
}
```

# MentorListDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Mentor_System
{
    // Response للـ GET /api/mentors
    public class MentorListDto
    {
        public List<MentorCardDto> Mentors { get; set; } = new();
    }
}
```

# RequestSessionRequest.cs.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Mentor_System
{

    // Request للـ POST /api/sessions/request
    public class RequestSessionRequest
    {
        public Guid MentorId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
```

# ReviewDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Mentor_System
{

    public class ReviewDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewedAt { get; set; }
    }
}
```

# RecommendationDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Personality_Test
{
    public class RecommendationDto
    {
        public Guid TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public decimal CompatibilityScore { get; set; }
        public int Rank { get; set; }
    }
}
```

# SaveTestProgressRequest.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Request للـ POST /api/test/save-progress
    public class SaveTestProgressRequest
    {
        public Guid UserId { get; set; }
        public Dictionary<string, int> Answers { get; set; } = new();
        public int CurrentPage { get; set; }
    }
}
```

# SubmitTestRequest.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Request للـ POST /api/test/submit
    public class SubmitTestRequest
    {
        public Guid UserId { get; set; }
        public Dictionary<string, int> Answers { get; set; } = new();
    }

}
```

# TestProgressDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Response للـ GET /api/test/progress/:userId
    public class TestProgressDto
    {
        public Dictionary<string, int> Answers { get; set; } = new();
        public int CurrentPage { get; set; }
    }
}
```

# TestResultDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Response للـ POST /api/test/submit
    public class TestResultDto
    {
        public string CareerPath { get; set; } = string.Empty;
        public Dictionary<string, decimal> Score { get; set; } = new();
        public List<RecommendationDto> Recommendations { get; set; } = new();
    }
}
```

# CurrentPathDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class CurrentPathDto
    {
        public Guid PathId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public decimal Progress { get; set; }
    }
}
```

# MentorBasicDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class MentorBasicDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;
        public string? Image { get; set; }
    }
}
```

# MilestoneProgressDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class MilestoneProgressDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Progress { get; set; }
    }
}
```

# NotificationCountDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class NotificationCountDto
    {
        public int Unread { get; set; }
    }
}
```

# PathOverviewDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class PathOverviewDto
    {
        public List<MilestoneProgressDto> Overview { get; set; } = new();
    }
}
```

# QuickAccessDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class QuickAccessDto
    {
        public bool Roadmap { get; set; }
        public int SavedResources { get; set; }
        public int Achievements { get; set; }
    }
}
```

# UserMentorDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class UserMentorDto
    {
        public bool HasMentor { get; set; }
        public MentorBasicDto? Mentor { get; set; }
    }
}
```

# UserProfileDto.cs
```cs
﻿using System;

namespace Carrivo.Application.DTOs.User_Profile
{
    // Response للـ GET /user/profile
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Image { get; set; }
    }

}
```

# IAuthenticationService.cs
```cs
﻿using System;
using Carrivo.Application.DTOs.Auth.Requests;
using Carrivo.Application.DTOs.Auth.Responses;
using Carrivo.Application.DTOs.Common;

namespace Carrivo.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequest request, string? ipAddress = null);
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequest request, string? ipAddress = null);
        Task<ApiResponse<bool>> VerifyEmailAsync(VerifyOtpRequest request);
        Task<ApiResponse<bool>> ResendVerificationOtpAsync(ResendOtpRequest request, string? ipAddress = null);
        Task<ApiResponse<bool>> ForgotPasswordAsync(ForgotPasswordRequest request, string? ipAddress = null);
        Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request);
        Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequest request, string? ipAddress = null);
        Task<ApiResponse<bool>> LogoutAsync(string refreshToken, string? ipAddress = null);
    }
}
```

# IMentorService.cs
```cs
﻿using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Mentor_System;

namespace Carrivo.Application.Interfaces;

public interface IMentorService
{
    Task<ApiResponse<MentorListDto>> GetMentorsAsync(
        string? search = null,
        string? sort = null,
        string? experience = null,
        string? availability = null,
        decimal? rating = null,
        decimal? price = null);

    Task<ApiResponse<MentorDetailDto>> GetMentorByIdAsync(Guid mentorId);
    Task<ApiResponse<bool>> RequestSessionAsync(RequestSessionRequest request);
}```

# IPersonalityTestService.cs
```cs
﻿using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Personality_Test;


namespace Carrivo.Application.Interfaces;

public interface IPersonalityTestService
{
    Task<ApiResponse<bool>> SaveTestProgressAsync(SaveTestProgressRequest request);
    Task<ApiResponse<TestResultDto>> SubmitTestAsync(SubmitTestRequest request);
    Task<ApiResponse<TestProgressDto>> GetTestProgressAsync(Guid userId);
}```

# IUserService.cs
```cs
﻿using Carrivo.Application.DTOs.Common;

using Carrivo.Application.DTOs.User_Profile;

namespace Carrivo.Application.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(Guid userId);
    Task<ApiResponse<CurrentPathDto>> GetCurrentPathAsync(Guid userId);
    Task<ApiResponse<PathOverviewDto>> GetPathOverviewAsync(Guid userId);
    Task<ApiResponse<UserMentorDto>> GetUserMentorAsync(Guid userId);
    Task<ApiResponse<QuickAccessDto>> GetQuickAccessAsync(Guid userId);
    Task<ApiResponse<NotificationCountDto>> GetNotificationCountAsync(Guid userId);
}```

# AuthenticationService.cs
```cs
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
```

# OtpService.cs
```cs
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
```

# TokenService.cs
```cs
using Carrivo.Application.Settings;
using Carrivo.Core.Entities;
using Carrivo.Core.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Carrivo.Application.Services.Auth;

public class TokenService : ITokenService
{
    private readonly CarrivoDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public TokenService(CarrivoDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateAccessToken(Guid userId, string email, string userType, Dictionary<string, string>? additionalClaims = null)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, userType),
            new Claim("userType", userType)
        };

        // Add additional claims if provided
        if (additionalClaims != null)
        {
            foreach (var claim in additionalClaims)
            {
                claims.Add(new Claim(claim.Key, claim.Value));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public async Task<Guid?> ValidateRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token == null || !token.IsActive)
            return null;

        return token.UserId;
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken, string? reason = null, string? ipAddress = null)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token == null || token.IsRevoked)
            return;

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.RevocationReason = reason;

        await _context.SaveChangesAsync();
    }

    public async Task RevokeAllUserRefreshTokensAsync(Guid userId, string? reason = null)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.RevocationReason = reason ?? "All tokens revoked";
        }

        await _context.SaveChangesAsync();
    }

    public async Task CleanupExpiredTokensAsync()
    {
        var expiredTokens = await _context.RefreshTokens
            .Where(rt => rt.ExpiresAt < DateTime.UtcNow)
            .ToListAsync();

        _context.RefreshTokens.RemoveRange(expiredTokens);
        await _context.SaveChangesAsync();
    }
}
```

# EmailService.cs
```cs
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

            // 1. إعداد SmtpClient مع Host و Port
            using var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                EnableSsl = _emailSettings.EnableSsl,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword)
            };

            // 2. تأكد من استخدام طريقة التسليم عبر الشبكة (ضروري لـ Gmail)
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

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
        catch (SmtpException smtpEx) // 3. التقاط أخطاء SMTP المحددة للتشخيص
        {
            _logger.LogError(smtpEx,
                "SMTP Error sending email to {ToEmail}. Status: {StatusCode}. Message: {ErrorMessage}. Check App Password.",
                toEmail,
                smtpEx.StatusCode,
                smtpEx.Message);
            // رمي استثناء جديد برسالة أوضح للمستخدم
            throw new Exception($"فشل إرسال البريد عبر SMTP: {smtpEx.Message} (رمز الحالة: {smtpEx.StatusCode})", smtpEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ToEmail}. General Error: {ErrorMessage}", toEmail, ex.Message);
            throw new Exception($"فشل إرسال البريد: {ex.Message}", ex);
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
}```

# MentorService .cs
```cs
﻿using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Mentor_System;
using Carrivo.Application.Interfaces;
using Carrivo.Core.Entities;
using Carrivo.Core.Enums;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Carrivo.Application.Services.Mentor;

public class MentorService : IMentorService
{
    private readonly CarrivoDbContext _context;

    public MentorService(CarrivoDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<MentorListDto>> GetMentorsAsync(
        string? search = null,
        string? sort = null,
        string? experience = null,
        string? availability = null,
        decimal? rating = null,
        decimal? price = null)
    {
        var query = _context.MentorProfiles
            .Where(mp => !mp.IsDeleted)
            .Include(mp => mp.User)
            .Include(mp => mp.MentorTracks)
                .ThenInclude(mt => mt.Track)
            .AsQueryable();

        // Search Filter
        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(mp =>
                mp.User.FirstName.ToLower().Contains(searchLower) ||
                mp.User.LastName.ToLower().Contains(searchLower) ||
                mp.JobTitle.ToLower().Contains(searchLower));
        }

        // Experience Filter
        if (!string.IsNullOrWhiteSpace(experience))
        {
            query = experience.ToLower() switch
            {
                "junior" => query.Where(mp => mp.YearsOfExperience <= 2),
                "mid" => query.Where(mp => mp.YearsOfExperience > 2 && mp.YearsOfExperience <= 5),
                "senior" => query.Where(mp => mp.YearsOfExperience > 5),
                _ => query
            };
        }

        // Rating Filter
        if (rating.HasValue)
        {
            query = query.Where(mp => mp.AverageRating >= rating.Value);
        }

        // Price Filter
        if (price.HasValue)
        {
            query = query.Where(mp => mp.PricePerHour <= price.Value);
        }

        // Sorting
        query = sort?.ToLower() switch
        {
            "rating" => query.OrderByDescending(mp => mp.AverageRating),
            "price_asc" => query.OrderBy(mp => mp.PricePerHour),
            "price_desc" => query.OrderByDescending(mp => mp.PricePerHour),
            "experience" => query.OrderByDescending(mp => mp.YearsOfExperience),
            _ => query.OrderByDescending(mp => mp.AverageRating) // default
        };

        var mentors = await query
            .Select(mp => new MentorCardDto
            {
                Id = mp.Id,
                Name = $"{mp.User.FirstName} {mp.User.LastName}",
                JobTitle = mp.JobTitle,
                Rating = mp.AverageRating,
                Price = mp.PricePerHour,
                Image = mp.User.ProfilePictureUrl,
                ExperienceLevel = mp.YearsOfExperience <= 2 ? "junior" :
                                 mp.YearsOfExperience <= 5 ? "mid" : "senior",
                Availability = "online", // TODO: إضافة logic للـ availability
                Track = mp.MentorTracks.FirstOrDefault() != null ?
                        mp.MentorTracks.First().Track.Code : ""
            })
            .ToListAsync();

        return ApiResponse<MentorListDto>.Success(new MentorListDto { Mentors = mentors });
    }

    public async Task<ApiResponse<MentorDetailDto>> GetMentorByIdAsync(Guid mentorId)
    {
        var mentor = await _context.MentorProfiles
            .Where(mp => mp.Id == mentorId && !mp.IsDeleted)
            .Include(mp => mp.User)
            .Include(mp => mp.MentorTracks)
                .ThenInclude(mt => mt.Track)
            .Include(mp => mp.MentorshipSessions)
                .ThenInclude(ms => ms.StudentProfile)
                    .ThenInclude(sp => sp.User)
            .FirstOrDefaultAsync();

        if (mentor == null)
            return ApiResponse<MentorDetailDto>.Failure("Mentor not found", statusCode: 404);

        // Get reviews from completed sessions
        var reviews = mentor.MentorshipSessions
            .Where(ms => ms.Status == MentorshipSessionStatus.Completed && ms.Rating.HasValue)
            .Select(ms => new ReviewDto
            {
                StudentId = ms.StudentProfile.UserId,
                StudentName = $"{ms.StudentProfile.User.FirstName} {ms.StudentProfile.User.LastName}",
                Rating = ms.Rating!.Value,
                Comment = ms.Review,
                ReviewedAt = ms.ReviewedAt ?? ms.UpdatedAt ?? ms.CreatedAt
            })
            .OrderByDescending(r => r.ReviewedAt)
            .ToList();

        // Extract skills from tracks
        var skills = mentor.MentorTracks.Select(mt => mt.Track.Code).ToList();

        var result = new MentorDetailDto
        {
            Id = mentor.Id,
            Name = $"{mentor.User.FirstName} {mentor.User.LastName}",
            Bio = mentor.ProfessionalBio,
            Reviews = reviews,
            Skills = skills,
            ExperienceSummary = $"{mentor.YearsOfExperience} years of experience in {mentor.JobTitle}",
            Price = mentor.PricePerHour,
            Rating = mentor.AverageRating,
            Image = mentor.User.ProfilePictureUrl
        };

        return ApiResponse<MentorDetailDto>.Success(result);
    }

    public async Task<ApiResponse<bool>> RequestSessionAsync(RequestSessionRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<bool>.Failure("Student profile not found", statusCode: 404);

        var mentorProfile = await _context.MentorProfiles
            .FirstOrDefaultAsync(mp => mp.Id == request.MentorId && !mp.IsDeleted);

        if (mentorProfile == null)
            return ApiResponse<bool>.Failure("Mentor not found", statusCode: 404);

        // Get student's active track
        var activeRoadmap = await _context.StudentRoadmaps
            .Where(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive)
            .Include(sr => sr.Roadmap)
            .FirstOrDefaultAsync();

        if (activeRoadmap == null)
            return ApiResponse<bool>.Failure("No active learning path found", statusCode: 400);

        // Check if there's already a pending/active request with this mentor
        var existingRequest = await _context.MentorshipRequests
            .AnyAsync(mr => mr.StudentProfileId == studentProfile.Id
                && mr.MentorProfileId == request.MentorId
                && (mr.Status == MentorshipRequestStatus.Pending
                    || mr.Status == MentorshipRequestStatus.Accepted));

        if (existingRequest)
            return ApiResponse<bool>.Failure("You already have a pending or active request with this mentor", statusCode: 400);

        // Create mentorship request
        var mentorshipRequest = new MentorshipRequest
        {
            Id = Guid.NewGuid(),
            StudentProfileId = studentProfile.Id,
            MentorProfileId = request.MentorId,
            TrackId = activeRoadmap.Roadmap.TrackId,
            Message = request.Message,
            Status = MentorshipRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.MentorshipRequests.Add(mentorshipRequest);
        await _context.SaveChangesAsync();

        // TODO: Send notification to mentor

        return ApiResponse<bool>.Success(true, "Session request sent successfully");
    }
}```

# PersonalityTestService .cs
```cs
﻿using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Personality_Test;
using Carrivo.Application.Interfaces;
using Carrivo.Core.Entities;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Carrivo.Application.Services.Test;

public class PersonalityTestService : IPersonalityTestService
{
    private readonly CarrivoDbContext _context;

    public PersonalityTestService(CarrivoDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<bool>> SaveTestProgressAsync(SaveTestProgressRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<bool>.Failure("Student profile not found", statusCode: 404);

        // الحصول على أحدث test attempt غير مكتمل
        var testAttempt = await _context.StudentTestAttempts
            .Where(sta => sta.StudentProfileId == studentProfile.Id && !sta.IsCompleted)
            .OrderByDescending(sta => sta.StartedAt)
            .FirstOrDefaultAsync();

        // إذا لم يوجد، إنشاء واحد جديد
        if (testAttempt == null)
        {
            var latestTest = await _context.PersonalityTests
                .OrderByDescending(pt => pt.CreatedAt)
                .FirstOrDefaultAsync();

            if (latestTest == null)
                return ApiResponse<bool>.Failure("No personality test available", statusCode: 404);

            testAttempt = new StudentTestAttempt
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfile.Id,
                PersonalityTestId = latestTest.Id,
                StartedAt = DateTime.UtcNow,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.StudentTestAttempts.Add(testAttempt);
        }

        // حفظ Progress كـ JSON في MlModelOutput مؤقتاً
        var progressData = new
        {
            Answers = request.Answers,
            CurrentPage = request.CurrentPage
        };

        testAttempt.MlModelOutput = JsonSerializer.Serialize(progressData);
        testAttempt.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Success(true, "Progress saved successfully");
    }

    public async Task<ApiResponse<TestResultDto>> SubmitTestAsync(SubmitTestRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<bool>.Failure("Student profile not found", statusCode: 404);

        var testAttempt = await _context.StudentTestAttempts
            .Where(sta => sta.StudentProfileId == studentProfile.Id && !sta.IsCompleted)
            .OrderByDescending(sta => sta.StartedAt)
            .FirstOrDefaultAsync();

        if (testAttempt == null)
            return ApiResponse<TestResultDto>.Failure("No active test attempt found", statusCode: 404);

        // TODO: هنا يتم استدعاء ML Model لتحليل الإجابات
        // مؤقتاً سنستخدم logic بسيط

        // حساب النتيجة (مثال)
        var scores = new Dictionary<string, decimal>
        {
            { "Analytical", 0 },
            { "Creative", 0 },
            { "Social", 0 },
            { "Practical", 0 }
        };

        // Get top 3 personality types based on scores
        var topPersonalityType = await _context.PersonalityTypes
            .OrderBy(pt => Guid.NewGuid()) // مؤقتاً random
            .FirstOrDefaultAsync();

        if (topPersonalityType == null)
            return ApiResponse<TestResultDto>.Failure("No personality types configured", statusCode: 500);

        // Get track recommendations
        var recommendations = await _context.PersonalityTypeTracks
            .Where(ptt => ptt.PersonalityTypeId == topPersonalityType.Id)
            .Include(ptt => ptt.Track)
            .OrderByDescending(ptt => ptt.CompatibilityScore)
            .Take(3)
            .Select((ptt, index) => new RecommendationDto
            {
                TrackId = ptt.TrackId,
                TrackName = ptt.Track.Code,
                CompatibilityScore = ptt.CompatibilityScore,
                Rank = index + 1
            })
            .ToListAsync();

        // Mark test as completed
        testAttempt.IsCompleted = true;
        testAttempt.CompletedAt = DateTime.UtcNow;
        testAttempt.PersonalityTypeId = topPersonalityType.Id;
        testAttempt.MlModelOutput = JsonSerializer.Serialize(new
        {
            Scores = scores,
            PersonalityType = topPersonalityType.Code
        });

        // Save recommendations
        foreach (var rec in recommendations)
        {
            _context.TestRecommendations.Add(new TestRecommendation
            {
                Id = Guid.NewGuid(),
                StudentTestAttemptId = testAttempt.Id,
                TrackId = rec.TrackId,
                Score = rec.CompatibilityScore,
                Rank = rec.Rank,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();

        var result = new TestResultDto
        {
            CareerPath = topPersonalityType.Code,
            Score = scores,
            Recommendations = recommendations
        };

        return ApiResponse<TestResultDto>.Success(result, "Test submitted successfully");
    }

    public async Task<ApiResponse<TestProgressDto>> GetTestProgressAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<TestProgressDto>.Failure("Student profile not found", statusCode: 404);

        var testAttempt = await _context.StudentTestAttempts
            .Where(sta => sta.StudentProfileId == studentProfile.Id && !sta.IsCompleted)
            .OrderByDescending(sta => sta.StartedAt)
            .FirstOrDefaultAsync();

        if (testAttempt == null || string.IsNullOrEmpty(testAttempt.MlModelOutput))
        {
            return ApiResponse<TestProgressDto>.Success(new TestProgressDto
            {
                Answers = new Dictionary<string, int>(),
                CurrentPage = 1
            });
        }

        var progressData = JsonSerializer.Deserialize<TestProgressDto>(testAttempt.MlModelOutput);

        return ApiResponse<TestProgressDto>.Success(progressData!, "Progress retrieved successfully");
    }
}```

# UserService .cs
```cs
﻿using Carrivo.Application.DTOs.Common;

using Carrivo.Application.DTOs.User_Profile;
using Carrivo.Application.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Carrivo.Application.Services.User;

public class UserService : IUserService
{
    private readonly CarrivoDbContext _context;

    public UserService(CarrivoDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(Guid userId)
    {
        var user = await _context.Users
            .Where(u => u.Id == userId && !u.IsDeleted)
            .Select(u => new UserProfileDto
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email!,
                Image = u.ProfilePictureUrl
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return ApiResponse<UserProfileDto>.Failure("User not found", statusCode: 404);

        return ApiResponse<UserProfileDto>.Success(user);
    }

    public async Task<ApiResponse<CurrentPathDto>> GetCurrentPathAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<CurrentPathDto>.Failure("Student profile not found", statusCode: 404);

        var activeRoadmap = await _context.StudentRoadmaps
            .Where(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive && !sr.IsDeleted)
            .Include(sr => sr.Roadmap)
                .ThenInclude(r => r.Track)
            .OrderByDescending(sr => sr.CreatedAt)
            .FirstOrDefaultAsync();

        if (activeRoadmap == null)
            return ApiResponse<CurrentPathDto>.Failure("No active path found", statusCode: 404);

        var result = new CurrentPathDto
        {
            PathId = activeRoadmap.RoadmapId,
            Title = activeRoadmap.Roadmap.Track.Code,
            Description = activeRoadmap.Roadmap.Description,
            Image = null, // يمكن إضافة حقل Image للـ Track
            Progress = activeRoadmap.ProgressPercentage
        };

        return ApiResponse<CurrentPathDto>.Success(result);
    }

    public async Task<ApiResponse<PathOverviewDto>> GetPathOverviewAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<PathOverviewDto>.Failure("Student profile not found", statusCode: 404);

        var activeRoadmap = await _context.StudentRoadmaps
            .Where(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive && !sr.IsDeleted)
            .FirstOrDefaultAsync();

        if (activeRoadmap == null)
            return ApiResponse<PathOverviewDto>.Failure("No active path found", statusCode: 404);

        var milestones = await _context.RoadmapMilestones
            .Where(rm => rm.RoadmapId == activeRoadmap.RoadmapId && !rm.IsDeleted)
            .OrderBy(rm => rm.OrderIndex)
            .Select(rm => new
            {
                rm.Id,
                rm.Title,
                Tasks = rm.Tasks.Where(t => !t.IsDeleted).Select(t => t.Id).ToList()
            })
            .ToListAsync();

        var overview = new List<MilestoneProgressDto>();

        foreach (var milestone in milestones)
        {
            if (milestone.Tasks.Count == 0)
            {
                overview.Add(new MilestoneProgressDto
                {
                    Title = milestone.Title,
                    Progress = 0
                });
                continue;
            }

            var completedTasks = await _context.StudentProgress
                .Where(sp => sp.StudentProfileId == studentProfile.Id
                    && milestone.Tasks.Contains(sp.MilestoneTaskId)
                    && sp.IsCompleted)
                .CountAsync();

            var progress = (decimal)completedTasks / milestone.Tasks.Count * 100;

            overview.Add(new MilestoneProgressDto
            {
                Title = milestone.Title,
                Progress = Math.Round(progress, 0)
            });
        }

        return ApiResponse<PathOverviewDto>.Success(new PathOverviewDto { Overview = overview });
    }

    public async Task<ApiResponse<UserMentorDto>> GetUserMentorAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<UserMentorDto>.Failure("Student profile not found", statusCode: 404);

        var activeSession = await _context.MentorshipSessions
            .Where(ms => ms.StudentProfileId == studentProfile.Id
                && (ms.Status == Core.Enums.MentorshipSessionStatus.Active
                    || ms.Status == Core.Enums.MentorshipSessionStatus.PendingPayment)
                && !ms.IsDeleted)
            .Include(ms => ms.MentorProfile)
                .ThenInclude(mp => mp.User)
            .Include(ms => ms.MentorProfile)
                .ThenInclude(mp => mp.MentorTracks)
                    .ThenInclude(mt => mt.Track)
            .OrderByDescending(ms => ms.CreatedAt)
            .FirstOrDefaultAsync();

        if (activeSession == null)
        {
            return ApiResponse<UserMentorDto>.Success(new UserMentorDto
            {
                HasMentor = false,
                Mentor = null
            });
        }

        var mentor = activeSession.MentorProfile;
        var speciality = mentor.MentorTracks.FirstOrDefault()?.Track.Code ?? mentor.JobTitle;

        return ApiResponse<UserMentorDto>.Success(new UserMentorDto
        {
            HasMentor = true,
            Mentor = new MentorBasicDto
            {
                Id = mentor.Id,
                Name = $"{mentor.User.FirstName} {mentor.User.LastName}",
                Speciality = speciality,
                Image = mentor.User.ProfilePictureUrl
            }
        });
    }

    public async Task<ApiResponse<QuickAccessDto>> GetQuickAccessAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<QuickAccessDto>.Failure("Student profile not found", statusCode: 404);

        var hasRoadmap = await _context.StudentRoadmaps
            .AnyAsync(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive && !sr.IsDeleted);

        // TODO: إضافة جدول للـ Saved Resources
        var savedResources = 0;

        // TODO: إضافة جدول للـ Achievements
        var achievements = 0;

        return ApiResponse<QuickAccessDto>.Success(new QuickAccessDto
        {
            Roadmap = hasRoadmap,
            SavedResources = savedResources,
            Achievements = achievements
        });
    }

    public async Task<ApiResponse<NotificationCountDto>> GetNotificationCountAsync(Guid userId)
    {
        var unreadCount = await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted)
            .CountAsync();

        return ApiResponse<NotificationCountDto>.Success(new NotificationCountDto
        {
            Unread = unreadCount
        });
    }
}```

# EmailSettings.cs
```cs
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
```

# JwtSettings.cs
```cs
namespace Carrivo.Application.Settings;

public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 15;
    public int RefreshTokenExpirationDays { get; set; } = 7;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateIssuerSigningKey { get; set; } = true;
    public int ClockSkewMinutes { get; set; } = 5;
}
```

# VerificationTypes.cs
```cs
namespace Carrivo.Core.Constants;

public static class VerificationTypes
{
    public const string EmailVerification = "EmailVerification";
    public const string PasswordReset = "PasswordReset";
}
```

# EmailVerification.cs
```cs
using System.ComponentModel.DataAnnotations;

namespace Carrivo.Core.Entities;

public class EmailVerification
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required, StringLength(6, MinimumLength = 6)]
    public string OtpCode { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    
    public bool IsUsed { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public int AttemptCount { get; set; } = 0;
    
    [MaxLength(50)]
    public string? RequestedFromIp { get; set; }
    
    [Required, MaxLength(50)]
    public string VerificationType { get; set; } = string.Empty; // "EmailVerification" or "PasswordReset"
    
    // Navigation Property
    public virtual Users User { get; set; } = null!;
    
    // Computed Property
    public bool IsValid => !IsUsed && DateTime.UtcNow < ExpiresAt && AttemptCount < 5;
}
```

# RefreshToken.cs
```cs
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
```

# BaseEntity.cs
```cs
namespace Carrivo.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}
```

# Message.cs
```cs
namespace Carrivo.Core.Entities;

public class Message : BaseEntity
{
    public Guid MentorshipSessionId { get; set; }
    public Guid? SenderStudentId { get; set; }
    public Guid? SenderMentorId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    
    // Navigation Properties
    public MentorshipSession MentorshipSession { get; set; } = null!;
    public StudentProfile? SenderStudent { get; set; }
    public MentorProfile? SenderMentor { get; set; }
    public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
}
```

# MessageAttachment.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MessageAttachment : BaseEntity
{
    public Guid MessageId { get; set; }
    public MessageAttachmentType AttachmentType { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public long? FileSizeBytes { get; set; }
    
    // Navigation Properties
    public Message Message { get; set; } = null!;
}
```

# MentorshipRequest.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MentorshipRequest : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    public string Message { get; set; } = string.Empty;
    public MentorshipRequestStatus Status { get; set; } = MentorshipRequestStatus.Pending;
    public DateTime? RespondedAt { get; set; }
    public string? ResponseMessage { get; set; }
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public MentorProfile MentorProfile { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public MentorshipSession? MentorshipSession { get; set; }
}
```

# MentorshipSession.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MentorshipSession : BaseEntity
{
    public Guid MentorshipRequestId { get; set; }
    public Guid StudentProfileId { get; set; }
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public MentorshipSessionStatus Status { get; set; } = MentorshipSessionStatus.PendingPayment;
    
    // Payment Info
    public bool IsPaid { get; set; } = false;
    public decimal? PaidAmount { get; set; }
    public DateTime? PaidAt { get; set; }
    
    // Rating & Review (after completion)
    public int? Rating { get; set; }
    public string? Review { get; set; }
    public DateTime? ReviewedAt { get; set; }
    
    // Navigation Properties
    public MentorshipRequest MentorshipRequest { get; set; } = null!;
    public StudentProfile StudentProfile { get; set; } = null!;
    public MentorProfile MentorProfile { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
```

# Notification.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    
    // Navigation Properties
    public Users User { get; set; } = null!;
}
```

# PersonalityQuestion.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class PersonalityQuestion : BaseEntity
{
    public Guid PersonalityTestId { get; set; }
    public string Question { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public string? OptionsJson { get; set; }
    public int OrderIndex { get; set; }
    
    // Navigation Properties
    public PersonalityTest PersonalityTest { get; set; } = null!;
    public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
```

# PersonalityTest.cs
```cs
namespace Carrivo.Core.Entities;

public class PersonalityTest : BaseEntity
{
    public string Version { get; set; } = "1.0";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Navigation Properties
    public ICollection<PersonalityQuestion> Questions { get; set; } = new List<PersonalityQuestion>();
    public ICollection<StudentTestAttempt> TestAttempts { get; set; } = new List<StudentTestAttempt>();
}
```

# PersonalityType.cs
```cs
namespace Carrivo.Core.Entities;

public class PersonalityType : BaseEntity
{
    public string Code { get; set; } = string.Empty; // e.g., "ANALYTICAL", "CREATIVE"
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true; // Soft Delete
    
    // Navigation Properties
    public ICollection<StudentTestAttempt> StudentTestAttempts { get; set; } = new List<StudentTestAttempt>();
    public ICollection<PersonalityTypeTrack> PersonalityTypeTracks { get; set; } = new List<PersonalityTypeTrack>();
}
```

# PersonalityTypeTrack.cs
```cs
namespace Carrivo.Core.Entities;

public class PersonalityTypeTrack : BaseEntity
{
    public Guid PersonalityTypeId { get; set; }
    public Guid TrackId { get; set; }
    public decimal CompatibilityScore { get; set; } // 0-100
    
    // Navigation Properties
    public PersonalityType PersonalityType { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
```

# StudentAnswer.cs
```cs
namespace Carrivo.Core.Entities;

public class StudentAnswer : BaseEntity
{
    public Guid StudentTestAttemptId { get; set; }
    public Guid PersonalityQuestionId { get; set; }
    public string AnswerValue { get; set; } = string.Empty; // Could be text, number, or JSON
    
    // Navigation Properties
    public StudentTestAttempt StudentTestAttempt { get; set; } = null!;
    public PersonalityQuestion PersonalityQuestion { get; set; } = null!;
}
```

# StudentTestAttempt.cs
```cs
namespace Carrivo.Core.Entities;

public class StudentTestAttempt : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid PersonalityTestId { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; } = false;
    
    // ML Model Output
    public Guid? PersonalityTypeId { get; set; }
    public string? MlModelOutput { get; set; } // JSON with scores and recommendations
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public PersonalityTest PersonalityTest { get; set; } = null!;
    public PersonalityType? PersonalityType { get; set; }
    public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    public ICollection<TestRecommendation> TestRecommendations { get; set; } = new List<TestRecommendation>();
}
```

# TestRecommendation.cs
```cs
namespace Carrivo.Core.Entities;

// Top 3 recommended tracks for a test attempt
public class TestRecommendation : BaseEntity
{
    public Guid StudentTestAttemptId { get; set; }
    public Guid TrackId { get; set; }
    public decimal Score { get; set; } // ML model score
    public int Rank { get; set; } // 1, 2, 3
    
    // Navigation Properties
    public StudentTestAttempt StudentTestAttempt { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
```

# MilestoneTask.cs
```cs
namespace Carrivo.Core.Entities;

public class MilestoneTask : BaseEntity
{
    public Guid RoadmapMilestoneId { get; set; }
    public string Title { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int? EstimatedHours { get; set; }
    public bool IsOptional { get; set; } = false;
    
    // Navigation Properties
    public RoadmapMilestone RoadmapMilestone { get; set; } = null!;
    public ICollection<TaskResource> Resources { get; set; } = new List<TaskResource>();
    public ICollection<StudentProgress> StudentProgress { get; set; } = new List<StudentProgress>();
}
```

# Roadmap.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class Roadmap : BaseEntity
{
    public Guid TrackId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TrackLevel Level { get; set; }
    public RoadmapCreatorType CreatorType { get; set; }
    public int? EstimatedDurationDays { get; set; }
    
    // Navigation Properties
    public Track Track { get; set; } = null!;
    public ICollection<RoadmapMilestone> Milestones { get; set; } = new List<RoadmapMilestone>();
    public ICollection<StudentRoadmap> StudentRoadmaps { get; set; } = new List<StudentRoadmap>();
}
```

# RoadmapMilestone.cs
```cs
namespace Carrivo.Core.Entities;

public class RoadmapMilestone : BaseEntity
{
    public Guid RoadmapId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int? EstimatedDurationDays { get; set; }
    
    // Navigation Properties
    public Roadmap Roadmap { get; set; } = null!;
    public ICollection<MilestoneTask> Tasks { get; set; } = new List<MilestoneTask>();
}
```

# StudentProgress.cs
```cs
namespace Carrivo.Core.Entities;

public class StudentProgress : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid MilestoneTaskId { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedAt { get; set; }
    public decimal? TimeSpentHours { get; set; }
    public string? Notes { get; set; }
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public MilestoneTask MilestoneTask { get; set; } = null!;
}
```

# StudentRoadmap.cs
```cs
namespace Carrivo.Core.Entities;

// Student's customized roadmap instance
public class StudentRoadmap : BaseEntity
{
    public Guid StudentProfileId { get; set; }
    public Guid RoadmapId { get; set; }
    public Guid? CreatedByMentorId { get; set; } // If created by mentor
    public DateTime StartDate { get; set; }
    public DateTime? TargetEndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal ProgressPercentage { get; set; } = 0;
    
    // Customization based on student data
    public decimal? DailyStudyHours { get; set; }
    public string? CustomizationNotes { get; set; }
    
    // Navigation Properties
    public StudentProfile StudentProfile { get; set; } = null!;
    public Roadmap Roadmap { get; set; } = null!;
    public MentorProfile? CreatedByMentor { get; set; }
}
```

# TaskResource.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class TaskResource : BaseEntity
{
    public Guid MilestoneTaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ResourceType ResourceType { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsFree { get; set; } = true;
    public int OrderIndex { get; set; }
    
    // Navigation Properties
    public MilestoneTask MilestoneTask { get; set; } = null!;
}
```

# MentorTrack.cs
```cs
namespace Carrivo.Core.Entities;

// Many-to-Many: Mentor specializes in Tracks
public class MentorTrack : BaseEntity
{
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    
    // Navigation Properties
    public MentorProfile MentorProfile { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
```

# MentorTrackLevel.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

// Mentor can teach specific levels
public class MentorTrackLevel : BaseEntity
{
    public Guid MentorProfileId { get; set; }
    public Guid TrackId { get; set; }
    public TrackLevel Level { get; set; }
    
    // Navigation Properties
    public MentorProfile MentorProfile { get; set; } = null!;
}
```

# Track.cs
```cs
namespace Carrivo.Core.Entities;

public class Track : BaseEntity
{
    public string Code { get; set; } = string.Empty; // e.g., "BACKEND", "FRONTEND"
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation Properties
    public ICollection<PersonalityTypeTrack> PersonalityTypeTracks { get; set; } = new List<PersonalityTypeTrack>();
    public ICollection<MentorTrack> MentorTracks { get; set; } = new List<MentorTrack>();
    public ICollection<Roadmap> Roadmaps { get; set; } = new List<Roadmap>();
    public ICollection<TestRecommendation> TestRecommendations { get; set; } = new List<TestRecommendation>();
}
```

# MentorProfile.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class MentorProfile : BaseEntity
{
    // Foreign Key
    public Guid UserId { get; set; }
    
    // Professional Data
    public string JobTitle { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string ProfessionalBio { get; set; } = string.Empty;
    
    // Mentorship Data
    public string? AvailabilitySchedule { get; set; }
    public decimal? PricePerHour { get; set; }
    public string? SpokenLanguages { get; set; }
    
    // Rating
    public decimal AverageRating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    
    // Navigation Properties
    public Users User { get; set; } = null!;
    public ICollection<MentorTrack> MentorTracks { get; set; } = new List<MentorTrack>();
    public ICollection<MentorTrackLevel> MentorTrackLevels { get; set; } = new List<MentorTrackLevel>();
    public ICollection<MentorshipRequest> MentorshipRequests { get; set; } = new List<MentorshipRequest>();
    public ICollection<MentorshipSession> MentorshipSessions { get; set; } = new List<MentorshipSession>();
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}
```

# StudentProfile.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class StudentProfile : BaseEntity
{
    // Foreign Key
    public Guid UserId { get; set; }
    
    // Academic & Professional Data
    public string? Education { get; set; }
    public EmploymentStatus? EmploymentStatus { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Experience { get; set; }
    public string? CvUrl { get; set; }
    
    // Learning Related Data
    public decimal? DailyStudyHours { get; set; }
    public string? CurrentSkills { get; set; }
    public string? PreferredFields { get; set; }
    public LearningStyle? PreferredLearningStyle { get; set; }
    
    // Navigation Properties
    public Users User { get; set; } = null!;
    public ICollection<StudentTestAttempt> TestAttempts { get; set; } = new List<StudentTestAttempt>();
    public ICollection<StudentRoadmap> StudentRoadmaps { get; set; } = new List<StudentRoadmap>();
    public ICollection<StudentProgress> StudentProgress { get; set; } = new List<StudentProgress>();
    public ICollection<MentorshipRequest> MentorshipRequests { get; set; } = new List<MentorshipRequest>();
    public ICollection<MentorshipSession> MentorshipSessions { get; set; } = new List<MentorshipSession>();
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}
```

# Users.cs
```cs
using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class Users : IdentityUser<Guid>
{
    // Basic Info
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    // User Type
    public UserType UserType { get; set; }
    
    // Authentication Fields
    public DateTime? EmailVerifiedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? LastLoginIp { get; set; }
    
    // OAuth Fields (for future Google/Facebook login)
    public string? OAuthProvider { get; set; }
    public string? OAuthProviderId { get; set; }
    public bool IsOAuthAccount { get; set; } = false;
    
    // Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    
    // Navigation Properties
    public StudentProfile? StudentProfile { get; set; }
    public MentorProfile? MentorProfile { get; set; }
    public ICollection<EmailVerification> EmailVerifications { get; set; } = new List<EmailVerification>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
```

# EmploymentStatus.cs
```cs
namespace Carrivo.Core.Enums;

public enum EmploymentStatus
{
    Student = 1,
    Employed = 2,
    Unemployed = 3,
    Freelancer = 4
}
```

# Gender.cs
```cs
namespace Carrivo.Core.Enums;

public enum Gender
{
    Male = 1,
    Female = 2
}
```

# LearningStyle.cs
```cs
namespace Carrivo.Core.Enums;

public enum LearningStyle
{
    Videos = 1,
    Reading = 2,
    Projects = 3,
    Interactive = 4
}
```

# MentorshipRequestStatus.cs
```cs
namespace Carrivo.Core.Enums;

public enum MentorshipRequestStatus
{
    Pending = 1,
    Accepted = 2,
    Rejected = 3,
    Cancelled = 4
}
```

# MentorshipSessionStatus.cs
```cs
namespace Carrivo.Core.Enums;

public enum MentorshipSessionStatus
{
    PendingPayment = 1,
    Active = 2,
    Completed = 3,
    Cancelled = 4
}
```

# MessageAttachmentType.cs
```cs
namespace Carrivo.Core.Enums;

public enum MessageAttachmentType
{
    Image = 1,
    Document = 2,
    Link = 3,
    Other = 4
}
```

# NotificationType.cs
```cs
namespace Carrivo.Core.Enums;

public enum NotificationType
{
    MentorshipRequest = 1,
    MentorshipAccepted = 2,
    MentorshipRejected = 3,
    NewMessage = 4,
    TaskCompleted = 5,
    MilestoneCompleted = 6,
    RoadmapCreated = 7,
    General = 8
}
```

# QuestionType.cs
```cs
namespace Carrivo.Core.Enums;

public enum QuestionType
{
    MultipleChoice = 1,
    Scale = 2,
    YesNo = 3
}
```

# ResourceType.cs
```cs
namespace Carrivo.Core.Enums;

public enum ResourceType
{
    Video = 1,
    Article = 2,
    Book = 3,
    Course = 4,
    Documentation = 5,
    Project = 6,
    Tool = 7,
    Other = 8
}
```

# RoadmapCreatorType.cs
```cs
namespace Carrivo.Core.Enums;

public enum RoadmapCreatorType
{
    System = 1,
    Student = 2,
    Mentor = 3
}
```

# TrackLevel.cs
```cs
namespace Carrivo.Core.Enums;

public enum TrackLevel
{
    Beginner = 1,
    Intermediate = 2,
    Advanced = 3
}
```

# UserType.cs
```cs
namespace Carrivo.Core.Enums;

public enum UserType
{
    Student = 1,
    Mentor = 2
}
```

# IEmailService.cs
```cs
namespace Carrivo.Core.Interfaces;

public interface IEmailService
{
    Task SendVerificationOtpAsync(string toEmail, string userName, string otpCode);
    Task SendPasswordResetOtpAsync(string toEmail, string userName, string otpCode);
    Task SendPasswordChangedNotificationAsync(string toEmail, string userName);
    Task SendWelcomeEmailAsync(string toEmail, string userName);
}
```

# IOtpService.cs
```cs
namespace Carrivo.Core.Interfaces;

public interface IOtpService
{
    Task<string> GenerateAndStoreOtpAsync(Guid userId, string email, string verificationType, string? ipAddress = null);
    Task<bool> ValidateOtpAsync(string email, string otpCode, string verificationType);
    Task<bool> CanResendOtpAsync(string email);
    Task InvalidateAllOtpsAsync(Guid userId, string verificationType);
}
```

# ITokenService.cs
```cs
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
```

# CarrivoDbContext.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Carrivo.Infrastructure.Data;

public class CarrivoDbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid>
{
    public CarrivoDbContext(DbContextOptions<CarrivoDbContext> options) 
        : base(options)
    {
    }

    // User Profiles
    public DbSet<StudentProfile> StudentProfiles { get; set; }
    public DbSet<MentorProfile> MentorProfiles { get; set; }

    // Authentication
    public DbSet<EmailVerification> EmailVerifications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // Personality Test
    public DbSet<PersonalityType> PersonalityTypes { get; set; }
    public DbSet<PersonalityTest> PersonalityTests { get; set; }
    public DbSet<PersonalityQuestion> PersonalityQuestions { get; set; }
    public DbSet<StudentTestAttempt> StudentTestAttempts { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }

    // Tracks & Recommendations
    public DbSet<Track> Tracks { get; set; }
    public DbSet<PersonalityTypeTrack> PersonalityTypeTracks { get; set; }
    public DbSet<TestRecommendation> TestRecommendations { get; set; }
    public DbSet<MentorTrack> MentorTracks { get; set; }
    public DbSet<MentorTrackLevel> MentorTrackLevels { get; set; }

    // Roadmaps
    public DbSet<Roadmap> Roadmaps { get; set; }
    public DbSet<StudentRoadmap> StudentRoadmaps { get; set; }
    public DbSet<RoadmapMilestone> RoadmapMilestones { get; set; }
    public DbSet<MilestoneTask> MilestoneTasks { get; set; }
    public DbSet<TaskResource> TaskResources { get; set; }
    public DbSet<StudentProgress> StudentProgress { get; set; }

    // Mentorship
    public DbSet<MentorshipRequest> MentorshipRequests { get; set; }
    public DbSet<MentorshipSession> MentorshipSessions { get; set; }

    // Chat
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageAttachment> MessageAttachments { get; set; }

    // Notifications
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarrivoDbContext).Assembly);
    }
}
```

# MessageAttachmentConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Communication;

public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
{
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.HasKey(ma => ma.Id);

        // Properties
        builder.Property(ma => ma.Url)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(ma => ma.FileName)
            .HasMaxLength(255);

        // Indexes
        builder.HasIndex(ma => ma.MessageId);
        builder.HasIndex(ma => ma.AttachmentType);

        // Relationships
        builder.HasOne(ma => ma.Message)
            .WithMany(m => m.Attachments)
            .HasForeignKey(ma => ma.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# MessageConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Communication;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        // Properties
        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(5000);

        // Indexes
        builder.HasIndex(m => m.MentorshipSessionId);
        builder.HasIndex(m => m.SenderStudentId);
        builder.HasIndex(m => m.SenderMentorId);
        builder.HasIndex(m => new { m.MentorshipSessionId, m.CreatedAt });
        builder.HasIndex(m => m.IsRead);

        // Relationships
        builder.HasOne(m => m.MentorshipSession)
            .WithMany(ms => ms.Messages)
            .HasForeignKey(m => m.MentorshipSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.SenderStudent)
            .WithMany(sp => sp.SentMessages)
            .HasForeignKey(m => m.SenderStudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.SenderMentor)
            .WithMany(mp => mp.SentMessages)
            .HasForeignKey(m => m.SenderMentorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# EmailVerificationConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations;

public class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
{
    public void Configure(EntityTypeBuilder<EmailVerification> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.OtpCode)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(e => e.VerificationType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.RequestedFromIp)
            .HasMaxLength(50);

        // Indexes for performance
        builder.HasIndex(e => e.Email);
        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => new { e.Email, e.VerificationType, e.IsUsed });
        builder.HasIndex(e => e.ExpiresAt);

        // Relationship
        builder.HasOne(e => e.User)
            .WithMany(u => u.EmailVerifications)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# MentorshipRequestConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Mentorship;

public class MentorshipRequestConfiguration : IEntityTypeConfiguration<MentorshipRequest>
{
    public void Configure(EntityTypeBuilder<MentorshipRequest> builder)
    {
        builder.HasKey(mr => mr.Id);

        // Properties
        builder.Property(mr => mr.Message)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(mr => mr.ResponseMessage)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(mr => mr.StudentProfileId);
        builder.HasIndex(mr => mr.MentorProfileId);
        builder.HasIndex(mr => mr.TrackId);
        builder.HasIndex(mr => mr.Status);
        builder.HasIndex(mr => new { mr.StudentProfileId, mr.Status });
        builder.HasIndex(mr => new { mr.MentorProfileId, mr.Status });

        // Relationships
        builder.HasOne(mr => mr.StudentProfile)
            .WithMany(sp => sp.MentorshipRequests)
            .HasForeignKey(mr => mr.StudentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.MentorProfile)
            .WithMany(mp => mp.MentorshipRequests)
            .HasForeignKey(mr => mr.MentorProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.Track)
            .WithMany()
            .HasForeignKey(mr => mr.TrackId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.MentorshipSession)
            .WithOne(ms => ms.MentorshipRequest)
            .HasForeignKey<MentorshipSession>(ms => ms.MentorshipRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# MentorshipSessionConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Mentorship;

public class MentorshipSessionConfiguration : IEntityTypeConfiguration<MentorshipSession>
{
    public void Configure(EntityTypeBuilder<MentorshipSession> builder)
    {
        builder.HasKey(ms => ms.Id);

        // Properties
        builder.Property(ms => ms.PaidAmount)
            .HasPrecision(10, 2);

        builder.Property(ms => ms.Review)
            .HasMaxLength(2000);

        builder.Property(ms => ms.Rating)
            .HasMaxLength(1); // 1-5 stars

        // Indexes
        builder.HasIndex(ms => ms.MentorshipRequestId)
            .IsUnique();

        builder.HasIndex(ms => ms.StudentProfileId);
        builder.HasIndex(ms => ms.MentorProfileId);
        builder.HasIndex(ms => ms.TrackId);
        builder.HasIndex(ms => ms.Status);
        builder.HasIndex(ms => new { ms.StudentProfileId, ms.Status });
        builder.HasIndex(ms => new { ms.MentorProfileId, ms.Status });
        builder.HasIndex(ms => ms.IsPaid);

        // Relationships
        builder.HasOne(ms => ms.StudentProfile)
            .WithMany(sp => sp.MentorshipSessions)
            .HasForeignKey(ms => ms.StudentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.MentorProfile)
            .WithMany(mp => mp.MentorshipSessions)
            .HasForeignKey(ms => ms.MentorProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.Track)
            .WithMany()
            .HasForeignKey(ms => ms.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# NotificationConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Notifications;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        // Properties
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(n => n.UserId);
        builder.HasIndex(n => n.Type);
        builder.HasIndex(n => n.IsRead);
        builder.HasIndex(n => new { n.UserId, n.IsRead });
        builder.HasIndex(n => new { n.UserId, n.CreatedAt });

        // Relationships
        builder.HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# PersonalityQuestionConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityQuestionConfiguration : IEntityTypeConfiguration<PersonalityQuestion>
{
    public void Configure(EntityTypeBuilder<PersonalityQuestion> builder)
    {
        builder.HasKey(pq => pq.Id);

        // Properties
        builder.Property(pq => pq.Question)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(pq => pq.OptionsJson)
            .HasMaxLength(4000);

        // Indexes
        builder.HasIndex(pq => pq.PersonalityTestId);
        builder.HasIndex(pq => new { pq.PersonalityTestId, pq.OrderIndex });

        // Relationships
        builder.HasOne(pq => pq.PersonalityTest)
            .WithMany(pt => pt.Questions)
            .HasForeignKey(pq => pq.PersonalityTestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# PersonalityTestConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityTestConfiguration : IEntityTypeConfiguration<Core.Entities.PersonalityTest>
{
    public void Configure(EntityTypeBuilder<Core.Entities.PersonalityTest> builder)
    {
        builder.HasKey(pt => pt.Id);

        // Properties
        builder.Property(pt => pt.Version)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(pt => pt.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(pt => pt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(pt => pt.Version);
    }
}
```

# PersonalityTypeConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityTypeConfiguration : IEntityTypeConfiguration<PersonalityType>
{
    public void Configure(EntityTypeBuilder<PersonalityType> builder)
    {
        builder.HasKey(pt => pt.Id);

        // Properties
        builder.Property(pt => pt.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pt => pt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(pt => pt.Code)
            .IsUnique();

        builder.HasIndex(pt => pt.IsActive);
    }
}
```

# PersonalityTypeTrackConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class PersonalityTypeTrackConfiguration : IEntityTypeConfiguration<PersonalityTypeTrack>
{
    public void Configure(EntityTypeBuilder<PersonalityTypeTrack> builder)
    {
        builder.HasKey(ptt => ptt.Id);

        // Properties
        builder.Property(ptt => ptt.CompatibilityScore)
            .HasPrecision(5, 2);

        // Indexes
        builder.HasIndex(ptt => ptt.PersonalityTypeId);
        builder.HasIndex(ptt => ptt.TrackId);
        builder.HasIndex(ptt => new { ptt.PersonalityTypeId, ptt.TrackId })
            .IsUnique();

        // Relationships
        builder.HasOne(ptt => ptt.PersonalityType)
            .WithMany(pt => pt.PersonalityTypeTracks)
            .HasForeignKey(ptt => ptt.PersonalityTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ptt => ptt.Track)
            .WithMany(t => t.PersonalityTypeTracks)
            .HasForeignKey(ptt => ptt.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# StudentAnswerConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
{
    public void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        builder.HasKey(sa => sa.Id);

        // Properties
        builder.Property(sa => sa.AnswerValue)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(sa => sa.StudentTestAttemptId);
        builder.HasIndex(sa => sa.PersonalityQuestionId);
        builder.HasIndex(sa => new { sa.StudentTestAttemptId, sa.PersonalityQuestionId })
            .IsUnique();

        // Relationships
        builder.HasOne(sa => sa.StudentTestAttempt)
            .WithMany(sta => sta.StudentAnswers)
            .HasForeignKey(sa => sa.StudentTestAttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sa => sa.PersonalityQuestion)
            .WithMany(pq => pq.StudentAnswers)
            .HasForeignKey(sa => sa.PersonalityQuestionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# StudentTestAttemptConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class StudentTestAttemptConfiguration : IEntityTypeConfiguration<StudentTestAttempt>
{
    public void Configure(EntityTypeBuilder<StudentTestAttempt> builder)
    {
        builder.HasKey(sta => sta.Id);

        // Properties
        builder.Property(sta => sta.MlModelOutput)
            .HasMaxLength(4000);

        // Indexes
        builder.HasIndex(sta => sta.StudentProfileId);
        builder.HasIndex(sta => sta.PersonalityTestId);
        builder.HasIndex(sta => sta.PersonalityTypeId);
        builder.HasIndex(sta => sta.IsCompleted);
        builder.HasIndex(sta => new { sta.StudentProfileId, sta.IsCompleted });
        builder.HasIndex(sta => new { sta.StudentProfileId, sta.CompletedAt });

        // Relationships
        builder.HasOne(sta => sta.StudentProfile)
            .WithMany(sp => sp.TestAttempts)
            .HasForeignKey(sta => sta.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sta => sta.PersonalityTest)
            .WithMany(pt => pt.TestAttempts)
            .HasForeignKey(sta => sta.PersonalityTestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sta => sta.PersonalityType)
            .WithMany(pt => pt.StudentTestAttempts)
            .HasForeignKey(sta => sta.PersonalityTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# TestRecommendationConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.PersonalityTest;

public class TestRecommendationConfiguration : IEntityTypeConfiguration<TestRecommendation>
{
    public void Configure(EntityTypeBuilder<TestRecommendation> builder)
    {
        builder.HasKey(tr => tr.Id);

        // Properties
        builder.Property(tr => tr.Score)
            .HasPrecision(5, 2);

        // Indexes
        builder.HasIndex(tr => tr.StudentTestAttemptId);
        builder.HasIndex(tr => tr.TrackId);
        builder.HasIndex(tr => new { tr.StudentTestAttemptId, tr.Rank });

        // Relationships
        builder.HasOne(tr => tr.StudentTestAttempt)
            .WithMany(sta => sta.TestRecommendations)
            .HasForeignKey(tr => tr.StudentTestAttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tr => tr.Track)
            .WithMany(t => t.TestRecommendations)
            .HasForeignKey(tr => tr.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# RefreshTokenConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token)
            .IsRequired();

        builder.Property(r => r.CreatedByIp)
            .HasMaxLength(50);

        builder.Property(r => r.RevokedByIp)
            .HasMaxLength(50);

        builder.Property(r => r.RevocationReason)
            .HasMaxLength(200);

        // Indexes for performance
        builder.HasIndex(r => r.Token)
            .IsUnique();
        
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => new { r.UserId, r.IsRevoked, r.ExpiresAt });

        // Relationship
        builder.HasOne(r => r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# MilestoneTaskConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class MilestoneTaskConfiguration : IEntityTypeConfiguration<MilestoneTask>
{
    public void Configure(EntityTypeBuilder<MilestoneTask> builder)
    {
        builder.HasKey(mt => mt.Id);

        // Properties
        builder.Property(mt => mt.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mt => mt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(mt => mt.RoadmapMilestoneId);
        builder.HasIndex(mt => new { mt.RoadmapMilestoneId, mt.OrderIndex });

        // Relationships
        builder.HasOne(mt => mt.RoadmapMilestone)
            .WithMany(rm => rm.Tasks)
            .HasForeignKey(mt => mt.RoadmapMilestoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# RoadmapConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class RoadmapConfiguration : IEntityTypeConfiguration<Roadmap>
{
    public void Configure(EntityTypeBuilder<Roadmap> builder)
    {
        builder.HasKey(r => r.Id);

        // Properties
        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(r => r.TrackId);
        builder.HasIndex(r => r.Level);
        builder.HasIndex(r => r.CreatorType);
        builder.HasIndex(r => new { r.TrackId, r.Level });

        // Relationships
        builder.HasOne(r => r.Track)
            .WithMany(t => t.Roadmaps)
            .HasForeignKey(r => r.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# RoadmapMilestoneConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class RoadmapMilestoneConfiguration : IEntityTypeConfiguration<RoadmapMilestone>
{
    public void Configure(EntityTypeBuilder<RoadmapMilestone> builder)
    {
        builder.HasKey(rm => rm.Id);

        // Properties
        builder.Property(rm => rm.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(rm => rm.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(rm => rm.RoadmapId);
        builder.HasIndex(rm => new { rm.RoadmapId, rm.OrderIndex });

        // Relationships
        builder.HasOne(rm => rm.Roadmap)
            .WithMany(r => r.Milestones)
            .HasForeignKey(rm => rm.RoadmapId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# StudentProgressConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class StudentProgressConfiguration : IEntityTypeConfiguration<StudentProgress>
{
    public void Configure(EntityTypeBuilder<StudentProgress> builder)
    {
        builder.HasKey(sp => sp.Id);

        // Properties
        builder.Property(sp => sp.TimeSpentHours)
            .HasPrecision(10, 2);

        builder.Property(sp => sp.Notes)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(sp => sp.StudentProfileId);
        builder.HasIndex(sp => sp.MilestoneTaskId);
        builder.HasIndex(sp => sp.IsCompleted);
        builder.HasIndex(sp => new { sp.StudentProfileId, sp.MilestoneTaskId })
            .IsUnique();
        builder.HasIndex(sp => new { sp.StudentProfileId, sp.IsCompleted });

        // Relationships
        builder.HasOne(sp => sp.StudentProfile)
            .WithMany(s => s.StudentProgress)
            .HasForeignKey(sp => sp.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sp => sp.MilestoneTask)
            .WithMany(mt => mt.StudentProgress)
            .HasForeignKey(sp => sp.MilestoneTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# StudentRoadmapConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class StudentRoadmapConfiguration : IEntityTypeConfiguration<StudentRoadmap>
{
    public void Configure(EntityTypeBuilder<StudentRoadmap> builder)
    {
        builder.HasKey(sr => sr.Id);

        // Properties
        builder.Property(sr => sr.ProgressPercentage)
            .HasPrecision(5, 2);

        builder.Property(sr => sr.DailyStudyHours)
            .HasPrecision(5, 2);

        builder.Property(sr => sr.CustomizationNotes)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(sr => sr.StudentProfileId);
        builder.HasIndex(sr => sr.RoadmapId);
        builder.HasIndex(sr => sr.CreatedByMentorId);
        builder.HasIndex(sr => sr.IsActive);
        builder.HasIndex(sr => new { sr.StudentProfileId, sr.IsActive });

        // Relationships
        builder.HasOne(sr => sr.StudentProfile)
            .WithMany(sp => sp.StudentRoadmaps)
            .HasForeignKey(sr => sr.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sr => sr.Roadmap)
            .WithMany(r => r.StudentRoadmaps)
            .HasForeignKey(sr => sr.RoadmapId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.CreatedByMentor)
            .WithMany()
            .HasForeignKey(sr => sr.CreatedByMentorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

# TaskResourceConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Roadmaps;

public class TaskResourceConfiguration : IEntityTypeConfiguration<TaskResource>
{
    public void Configure(EntityTypeBuilder<TaskResource> builder)
    {
        builder.HasKey(tr => tr.Id);

        // Properties
        builder.Property(tr => tr.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(tr => tr.Description)
            .HasMaxLength(1000);

        builder.Property(tr => tr.Url)
            .IsRequired()
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(tr => tr.MilestoneTaskId);
        builder.HasIndex(tr => tr.ResourceType);
        builder.HasIndex(tr => new { tr.MilestoneTaskId, tr.OrderIndex });

        // Relationships
        builder.HasOne(tr => tr.MilestoneTask)
            .WithMany(mt => mt.Resources)
            .HasForeignKey(tr => tr.MilestoneTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# MentorTrackConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Tracks;

public class MentorTrackConfiguration : IEntityTypeConfiguration<MentorTrack>
{
    public void Configure(EntityTypeBuilder<MentorTrack> builder)
    {
        builder.HasKey(mt => mt.Id);

        // Indexes
        builder.HasIndex(mt => mt.MentorProfileId);
        builder.HasIndex(mt => mt.TrackId);
        builder.HasIndex(mt => new { mt.MentorProfileId, mt.TrackId })
            .IsUnique();

        // Relationships
        builder.HasOne(mt => mt.MentorProfile)
            .WithMany(mp => mp.MentorTracks)
            .HasForeignKey(mt => mt.MentorProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mt => mt.Track)
            .WithMany(t => t.MentorTracks)
            .HasForeignKey(mt => mt.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# MentorTrackLevelConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Tracks;

public class MentorTrackLevelConfiguration : IEntityTypeConfiguration<MentorTrackLevel>
{
    public void Configure(EntityTypeBuilder<MentorTrackLevel> builder)
    {
        builder.HasKey(mtl => mtl.Id);

        // Indexes
        builder.HasIndex(mtl => mtl.MentorProfileId);
        builder.HasIndex(mtl => mtl.TrackId);
        builder.HasIndex(mtl => new { mtl.MentorProfileId, mtl.TrackId, mtl.Level })
            .IsUnique();

        // Relationships
        builder.HasOne(mtl => mtl.MentorProfile)
            .WithMany(mp => mp.MentorTrackLevels)
            .HasForeignKey(mtl => mtl.MentorProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# TrackConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Tracks;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(t => t.Code)
            .IsUnique();

        builder.HasIndex(t => t.IsActive);
    }
}
```

# MentorProfileConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Users;

public class MentorProfileConfiguration : IEntityTypeConfiguration<MentorProfile>
{
    public void Configure(EntityTypeBuilder<MentorProfile> builder)
    {
        builder.HasKey(mp => mp.Id);

        // Properties
        builder.Property(mp => mp.JobTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mp => mp.ProfessionalBio)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(mp => mp.AvailabilitySchedule)
            .HasMaxLength(1000);

        builder.Property(mp => mp.PricePerHour)
            .HasPrecision(10, 2);

        builder.Property(mp => mp.SpokenLanguages)
            .HasMaxLength(500);

        builder.Property(mp => mp.AverageRating)
            .HasPrecision(3, 2);

        // Indexes
        builder.HasIndex(mp => mp.UserId)
            .IsUnique();

        builder.HasIndex(mp => mp.AverageRating);
        builder.HasIndex(mp => mp.IsDeleted);

        // Relationships
        builder.HasOne(mp => mp.User)
            .WithOne(u => u.MentorProfile)
            .HasForeignKey<MentorProfile>(mp => mp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# StudentProfileConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Users;

public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.HasKey(sp => sp.Id);

        // Properties
        builder.Property(sp => sp.Education)
            .HasMaxLength(500);

        builder.Property(sp => sp.Experience)
            .HasMaxLength(2000);

        builder.Property(sp => sp.CvUrl)
            .HasMaxLength(500);

        builder.Property(sp => sp.CurrentSkills)
            .HasMaxLength(1000);

        builder.Property(sp => sp.PreferredFields)
            .HasMaxLength(1000);

        builder.Property(sp => sp.DailyStudyHours)
            .HasPrecision(5, 2);

        // Indexes
        builder.HasIndex(sp => sp.UserId)
            .IsUnique();

        builder.HasIndex(sp => sp.IsDeleted);

        // Relationships
        builder.HasOne(sp => sp.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<StudentProfile>(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

# UsersConfiguration.cs
```cs
using Carrivo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrivo.Infrastructure.Data.Configurations.Users;

public class UsersConfiguration : IEntityTypeConfiguration<Core.Entities.Users>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Users> builder)
    {
        // Properties
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(u => u.LastLoginIp)
            .HasMaxLength(50);

        builder.Property(u => u.OAuthProvider)
            .HasMaxLength(50);

        builder.Property(u => u.OAuthProviderId)
            .HasMaxLength(255);

        // Indexes
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.UserType);
        builder.HasIndex(u => u.IsDeleted);
        builder.HasIndex(u => new { u.Email, u.IsDeleted });

    }
}
```

# AuthController.cs
```cs
using Carrivo.Application.DTOs.Auth.Requests;
using Carrivo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrivo.Controllers;

/// <summary>
/// Responsible for all authentication and account management operations, 
/// including registration, login, email verification, and token refreshing.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user (Student or Mentor).
    /// </summary>
    /// <param name="request">New user data: name, email, password, and user type.</param>
    /// <returns>Status Code and the detailed result of the operation.</returns>
    /// <response code="200">Registration process successful.</response>
    /// <response code="400">If the input data is incorrect or the email is already in use.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.RegisterAsync(request, ipAddress);

        // Modification added here to reflect that 200 is the expected success status code from the service
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Logs in using email and password.
    /// </summary>
    /// <param name="request">The user's email and password.</param>
    /// <returns>Status Code and results including the Access Token and Refresh Token.</returns>
    /// <response code="200">Successful login and tokens returned.</response>
    /// <response code="400">If the input data is incorrect.</response>
    /// <response code="401">If the credentials are invalid or the account is not verified.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.LoginAsync(request, ipAddress);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Verifies email using the sent OTP code.
    /// </summary>
    /// <param name="request">The OTP code and email.</param>
    /// <returns>Status Code and the result of the operation.</returns>
    /// <response code="200">Email verification successful and account activated.</response>
    /// <response code="400">If the OTP code is incorrect or expired.</response>
    [HttpPost("verify-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyOtpRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.VerifyEmailAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Resends the OTP code for email verification.
    /// </summary>
    /// <param name="request">The email address to resend the code to.</param>
    /// <returns>Status Code and the result of the operation.</returns>
    /// <response code="200">Successfully resent the OTP code.</response>
    /// <response code="400">If the input data is incorrect.</response>
    /// <response code="429">If the user exceeded the maximum number of requests in a short time.</response>
    [HttpPost("resend-verification-otp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResendVerificationOtp([FromBody] ResendOtpRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.ResendVerificationOtpAsync(request, ipAddress);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Requests password reset (sends OTP code to the email).
    /// </summary>
    /// <param name="request">The email address of the account requiring password reset.</param>
    /// <returns>Status Code and the result of the operation.</returns>
    /// <response code="200">Successfully sent the OTP code.</response>
    /// <response code="400">If the input data is incorrect.</response>
    /// <response code="429">If the user exceeded the maximum number of requests.</response>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.ForgotPasswordAsync(request, ipAddress);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Resets the password using the OTP code.
    /// </summary>
    /// <param name="request">The OTP code, email, and the new password.</param>
    /// <returns>Status Code and the result of the operation.</returns>
    /// <response code="200">Password reset successful.</response>
    /// <response code="400">If the code is incorrect or the new password does not meet the requirements.</response>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ResetPasswordAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Refreshes the Access Token using the expired Refresh Token.
    /// </summary>
    /// <param name="request">The current Refresh Token.</param>
    /// <returns>Status Code, a new Access Token, and a new Refresh Token.</returns>
    /// <response code="200">Tokens successfully refreshed.</response>
    /// <response code="401">If the Refresh Token is invalid, revoked, or expired.</response>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.RefreshTokenAsync(request, ipAddress);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Logs out (revokes Refresh Token). Requires a valid token in the Header.
    /// </summary>
    /// <param name="request">The Refresh Token to be revoked.</param>
    /// <returns>Status Code and the result of the operation.</returns>
    /// <response code="200">Successful logout and token revocation.</response>
    /// <response code="400">If the input data is incorrect.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.LogoutAsync(request.RefreshToken, ipAddress);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets current user information (requires authentication).
    /// </summary>
    /// <returns>Essential user data extracted from the valid JWT token.</returns>
    /// <response code="200">Success and user data returned.</response>
    /// <response code="401">If the user is not authenticated or the token is expired.</response>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var userType = User.FindFirst("userType")?.Value;
        var firstName = User.FindFirst("firstName")?.Value;
        var lastName = User.FindFirst("lastName")?.Value;

        return Ok(new
        {
            userId,
            email,
            userType,
            firstName,
            lastName,
            claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }
}```

# MentorsController.cs
```cs
﻿using Carrivo.Application.DTOs.Mentor_System;
using Carrivo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrivo.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class MentorsController : ControllerBase
{
    private readonly IMentorService _mentorService;

    public MentorsController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }

    /// <summary>
    /// Gets list of mentors with optional filters
    /// GET /api/mentors?search=&sort=&experience=&availability=&rating=&price=
    /// </summary>
    /// <param name="search">Search by name or job title</param>
    /// <param name="sort">Sort by: rating, price_asc, price_desc, experience</param>
    /// <param name="experience">Filter by: junior, mid, senior</param>
    /// <param name="availability">Filter by: online, offline, busy</param>
    /// <param name="rating">Minimum rating (0-5)</param>
    /// <param name="price">Maximum price per hour</param>
    [HttpGet("mentors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMentors(
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? experience = null,
        [FromQuery] string? availability = null,
        [FromQuery] decimal? rating = null,
        [FromQuery] decimal? price = null)
    {
        var result = await _mentorService.GetMentorsAsync(
            search, sort, experience, availability, rating, price);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets detailed information about a specific mentor
    /// GET /api/mentors/{mentorId}
    /// </summary>
    [HttpGet("mentors/{mentorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMentorById(Guid mentorId)
    {
        var result = await _mentorService.GetMentorByIdAsync(mentorId);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Requests a mentorship session with a mentor
    /// POST /api/sessions/request
    /// </summary>
    [HttpPost("sessions/request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RequestSession([FromBody] RequestSessionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mentorService.RequestSessionAsync(request);
        return StatusCode(result.StatusCode, result);
    }
}```

# SessionsController.cs
```cs
﻿
using Carrivo.Application.DTOs.Mentor_System;
using Carrivo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrivo.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SessionsController : ControllerBase
{
    private readonly IMentorService _mentorService;

    public SessionsController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }

    /// <summary>
    /// Requests a mentorship session with a mentor
    /// </summary>
    [HttpPost("request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RequestSession([FromBody] RequestSessionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mentorService.RequestSessionAsync(request);
        return StatusCode(result.StatusCode, result);
    }
}```

# UserController.cs
```cs
﻿using Carrivo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrivo.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim!);
    }

    /// <summary>
    /// Gets current user profile
    /// GET /user/profile
    /// </summary>
    [HttpGet("user/profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfile()
    {
        var userId = GetCurrentUserId();
        var result = await _userService.GetUserProfileAsync(userId);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets user's current learning path
    /// GET /user/current-path
    /// </summary>
    [HttpGet("user/current-path")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentPath()
    {
        var userId = GetCurrentUserId();
        var result = await _userService.GetCurrentPathAsync(userId);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets overview of current path milestones with progress
    /// GET /user/current-path/overview
    /// </summary>
    [HttpGet("user/current-path/overview")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPathOverview()
    {
        var userId = GetCurrentUserId();
        var result = await _userService.GetPathOverviewAsync(userId);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets user's assigned mentor (if any)
    /// GET /user/mentor
    /// </summary>
    [HttpGet("user/mentor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMentor()
    {
        var userId = GetCurrentUserId();
        var result = await _userService.GetUserMentorAsync(userId);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets quick access statistics
    /// GET /user/quick-access
    /// </summary>
    [HttpGet("user/quick-access")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetQuickAccess()
    {
        var userId = GetCurrentUserId();
        var result = await _userService.GetQuickAccessAsync(userId);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets count of unread notifications
    /// GET /user/notifications/count
    /// </summary>
    [HttpGet("user/notifications/count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetNotificationCount()
    {
        var userId = GetCurrentUserId();
        var result = await _userService.GetNotificationCountAsync(userId);
        return StatusCode(result.StatusCode, result);
    }
}```

# AuthenticationExtensions.cs
```cs
using Carrivo.Application.Interfaces;
using Carrivo.Application.Services.Auth;
using Carrivo.Application.Services.Email;
using Carrivo.Application.Settings;
using Carrivo.Core.Entities;
using Carrivo.Core.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TaskManagement.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure Settings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        // Configure Identity
        services.AddIdentity<Users, IdentityRole<Guid>>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false; // We handle this manually with OTP
        })
        .AddEntityFrameworkStores<CarrivoDbContext>()
        .AddDefaultTokenProviders();

        // Configure JWT Authentication
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var key = Encoding.UTF8.GetBytes(jwtSettings!.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false; // Set to true in production
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifetime,
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.FromMinutes(jwtSettings.ClockSkewMinutes)
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        isSuccess = false,
                        message = "You are not authorized to access this resource",
                        statusCode = 401
                    });
                    return context.Response.WriteAsync(result);
                }
            };
        });

        // Register Services


        return services;
    }
}
```

# ServiceExtensions.cs
```cs
using Carrivo.Application.Interfaces;
using Carrivo.Application.Services.Auth;
using Carrivo.Application.Services.Email;
using Carrivo.Application.Services.Mentor;
using Carrivo.Application.Services.Test;
using Carrivo.Application.Services.User;
using Carrivo.Core.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CarrivoDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPersonalityTestService, PersonalityTestService>();

            services.AddScoped<IMentorService, MentorService>();


            return services;
        }
    }
}
```

# Program.cs
```cs
using SwaggerThemes;
using TaskManagement.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ����� ��� ��������� ������ ��� ����
builder.Configuration.AddJsonFile("appsettings.Development.Local.json", optional: true, reloadOnChange: true);

// ����� ������� ������� (Configuration)
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

// ����� Repositories � Services (Dependency Injection)
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers();

// Swagger/OpenAPI Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Carrivo API",
        Version = "v1",
        Description = "Educational platform API with authentication"
    });

    // ����� ������� JWT Authentication ��� Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // ������ ��� ���� ��� ������� XML
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// **********************************
// ����� CORS ������ ��� ���� (��� ��� ��� Production)
// **********************************
builder.Services.AddCors(options =>
{
    options.AddPolicy("CarrivoCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin() // <--- ���� ��� ���� �������
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// **********************************


var app = builder.Build();

// **********************************
// ����� Swagger � SwaggerUI ����� ������� (��� �� ��� Production)
// �������: ����� RoutePrefix ������ ������ Swagger ��� ������ ����� (/)
// **********************************
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Carrivo API V1");
    options.RoutePrefix = string.Empty; // <--- ��� �� ������� �����! ���� Swagger ��� /
  
});
// **********************************


app.UseHttpsRedirection();

// CORS
app.UseCors("CarrivoCorsPolicy");

// Authentication & Authorization (������� ���!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();```

