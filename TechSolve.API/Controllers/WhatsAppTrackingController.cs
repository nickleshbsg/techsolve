using Microsoft.AspNetCore.Mvc;
using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;
using TechSolve.Service.Interfaces;

namespace TechSolve.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WhatsAppTrackingController : ControllerBase
{
    private readonly IWhatsAppTrackingService _service;
    public WhatsAppTrackingController(IWhatsAppTrackingService service) => _service = service;

    /// <summary>Track a WhatsApp button click</summary>
    [HttpPost("track")]
    public async Task<IActionResult> Track([FromBody] TrackWhatsAppRequest request)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var ua = Request.Headers.UserAgent.ToString();
        await _service.TrackClickAsync(request, ip, ua);
        return Ok(ApiResponse<object>.Ok(new { tracked = true }, "Click tracked."));
    }

    /// <summary>Get WhatsApp analytics for a date range</summary>
    [HttpGet("analytics")]
    [ProducesResponseType(typeof(ApiResponse<WhatsAppAnalyticsResponse>), 200)]
    public async Task<IActionResult> Analytics([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var f = from ?? DateTime.UtcNow.AddDays(-30);
        var t = to ?? DateTime.UtcNow;
        var result = await _service.GetAnalyticsAsync(f, t);
        return Ok(ApiResponse<WhatsAppAnalyticsResponse>.Ok(result));
    }
}
