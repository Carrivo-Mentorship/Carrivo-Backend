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
}