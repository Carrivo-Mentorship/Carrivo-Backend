
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
}