using Microsoft.AspNetCore.Mvc;
using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;
using TechSolve.Service.Interfaces;

namespace TechSolve.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EnquiryController : ControllerBase
{
    private readonly IEnquiryService _service;
    public EnquiryController(IEnquiryService service) => _service = service;

    /// <summary>Submit a new enquiry from the website</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<EnquiryResponse>), 201)]
    [ProducesResponseType(typeof(ApiResponse<object>), 400)]
    public async Task<IActionResult> Create([FromBody] CreateEnquiryRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(ApiResponse<object>.Fail("Validation failed.", errors));
        }
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var ua = Request.Headers.UserAgent.ToString();
        var result = await _service.CreateAsync(request, ip, ua);
        return CreatedAtAction(nameof(GetByTicket), new { ticketId = result.TicketId },
            ApiResponse<EnquiryResponse>.Ok(result, "Enquiry submitted successfully."));
    }

    /// <summary>Get an enquiry by ticket ID</summary>
    [HttpGet("{ticketId}")]
    [ProducesResponseType(typeof(ApiResponse<EnquiryResponse>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByTicket(string ticketId)
    {
        var result = await _service.GetByTicketIdAsync(ticketId);
        return result is null
            ? NotFound(ApiResponse<object>.Fail($"Ticket '{ticketId}' not found."))
            : Ok(ApiResponse<EnquiryResponse>.Ok(result));
    }

    /// <summary>Get paginated list of enquiries</summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<EnquiryResponse>>), 200)]
    public async Task<IActionResult> GetPaged([FromQuery] GetEnquiriesRequest request)
    {
        var result = await _service.GetPagedAsync(request);
        return Ok(ApiResponse<PagedResponse<EnquiryResponse>>.Ok(result));
    }
}
