using Microsoft.AspNetCore.Mvc;
using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;
using TechSolve.Service.Interfaces;

namespace TechSolve.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _service;
    public BlogController(IBlogService service) => _service = service;

    /// <summary>Get published blog posts (paged)</summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<BlogSummaryResponse>>), 200)]
    public async Task<IActionResult> GetPublished([FromQuery] GetBlogRequest request)
    {
        var result = await _service.GetPublishedAsync(request);
        return Ok(ApiResponse<PagedResponse<BlogSummaryResponse>>.Ok(result));
    }

    /// <summary>Get a blog post by slug</summary>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ApiResponse<BlogDetailResponse>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var result = await _service.GetBySlugAsync(slug);
        return result is null
            ? NotFound(ApiResponse<object>.Fail($"Blog post '{slug}' not found."))
            : Ok(ApiResponse<BlogDetailResponse>.Ok(result));
    }

    /// <summary>Get posts by category</summary>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<BlogSummaryResponse>>), 200)]
    public async Task<IActionResult> GetByCategory(string category)
    {
        var result = await _service.GetByCategoryAsync(category);
        return Ok(ApiResponse<IEnumerable<BlogSummaryResponse>>.Ok(result));
    }
}
