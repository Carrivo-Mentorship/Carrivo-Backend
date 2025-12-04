using Carrivo.Application.Interfaces;
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
}