using Carrivo.Application.DTOs.Mentor_System;
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
}