using Carrivo.Application.DTOs.Personality_Test;
using Carrivo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrivo.Controllers;

[ApiController]
[Route("api/test")]
[Authorize]
public class TestController : ControllerBase
{
    private readonly IPersonalityTestService _testService;

    public TestController(IPersonalityTestService testService)
    {
        _testService = testService;
    }

    /// <summary>
    /// Saves test progress (partial answers)
    /// POST /api/test/save-progress
    /// </summary>
    [HttpPost("save-progress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SaveProgress([FromBody] SaveTestProgressRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _testService.SaveTestProgressAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Submits complete test and gets results
    /// POST /api/test/submit
    /// </summary>
    [HttpPost("submit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SubmitTest([FromBody] SubmitTestRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _testService.SubmitTestAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets saved test progress for user
    /// GET /api/test/progress/{userId}
    /// </summary>
    [HttpGet("progress/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProgress(Guid userId)
    {
        var result = await _testService.GetTestProgressAsync(userId);
        return StatusCode(result.StatusCode, result);
    }
}