using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
