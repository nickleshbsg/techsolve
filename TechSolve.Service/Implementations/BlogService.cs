using System.Text.Json;
using TechSolve.Domain.Entities;
using TechSolve.Domain.Interfaces;
using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;
using TechSolve.Domain.Constants;
using TechSolve.Service.Interfaces;

namespace TechSolve.Service.Implementations;

public class BlogService : IBlogService
{
    private readonly IBlogPostRepository _repo;
    public BlogService(IBlogPostRepository repo) => _repo = repo;

    public async Task<PagedResponse<BlogSummaryResponse>> GetPublishedAsync(GetBlogRequest req)
    {
        var page = Math.Max(1, req.Page);
        var size = Math.Clamp(req.PageSize, 1, AppConstants.Pagination.MaxPageSize);
        IEnumerable<BlogPost> items = req.Category is not null
            ? await _repo.GetByCategoryAsync(req.Category)
            : await _repo.GetPublishedAsync(page, size);
        var total = await _repo.CountAsync(p => p.IsPublished);
        return new PagedResponse<BlogSummaryResponse>
        {
            Items = items.Select(ToSummary),
            TotalCount = total,
            Page = page,
            PageSize = size
        };
    }

    public async Task<BlogDetailResponse?> GetBySlugAsync(string slug)
    {
        var p = await _repo.GetBySlugAsync(slug);
        if (p is null) return null;
        await _repo.IncrementViewCountAsync(p.Id);
        return ToDetail(p);
    }

    public async Task<IEnumerable<BlogSummaryResponse>> GetByCategoryAsync(string category) =>
        (await _repo.GetByCategoryAsync(category)).Select(ToSummary);

    private static BlogSummaryResponse ToSummary(BlogPost p) => new()
    {
        Id = p.Id, Slug = p.Slug, Title = p.Title, Excerpt = p.Excerpt,
        Category = p.Category, CoverImageUrl = p.CoverImageUrl,
        AuthorName = p.AuthorName, ReadTimeMinutes = p.ReadTimeMinutes,
        PublishedAt = p.PublishedAt, ViewCount = p.ViewCount
    };

    private static BlogDetailResponse ToDetail(BlogPost p) => new()
    {
        Id = p.Id, Slug = p.Slug, Title = p.Title, Excerpt = p.Excerpt,
        Content = p.Content, Category = p.Category, CoverImageUrl = p.CoverImageUrl,
        AuthorName = p.AuthorName, ReadTimeMinutes = p.ReadTimeMinutes,
        PublishedAt = p.PublishedAt, ViewCount = p.ViewCount
    };
}
