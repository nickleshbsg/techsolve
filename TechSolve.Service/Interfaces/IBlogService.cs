using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;

namespace TechSolve.Service.Interfaces;

public interface IBlogService
{
    Task<PagedResponse<BlogSummaryResponse>> GetPublishedAsync(GetBlogRequest request);
    Task<BlogDetailResponse?> GetBySlugAsync(string slug);
    Task<IEnumerable<BlogSummaryResponse>> GetByCategoryAsync(string category);
}
