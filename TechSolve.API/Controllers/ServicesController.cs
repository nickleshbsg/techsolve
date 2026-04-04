using Microsoft.AspNetCore.Mvc;
using TechSolve.Domain.Responses;
using TechSolve.Service.Interfaces;

namespace TechSolve.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ServicesController : ControllerBase
{
    private readonly IServiceCatalogService _service;
    public ServicesController(IServiceCatalogService service) => _service = service;

    /// <summary>Get all active services</summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ServiceSummaryResponse>>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllActiveAsync();
        return Ok(ApiResponse<IEnumerable<ServiceSummaryResponse>>.Ok(result));
    }

    /// <summary>Get service detail by slug</summary>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ApiResponse<ServiceDetailResponse>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var result = await _service.GetBySlugAsync(slug);
        return result is null
            ? NotFound(ApiResponse<object>.Fail($"Service '{slug}' not found."))
            : Ok(ApiResponse<ServiceDetailResponse>.Ok(result));
    }
}
