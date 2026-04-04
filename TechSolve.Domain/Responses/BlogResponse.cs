namespace TechSolve.Domain.Responses;

public class BlogSummaryResponse
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int ReadTimeMinutes { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int ViewCount { get; set; }
}

public class BlogDetailResponse : BlogSummaryResponse
{
    public string Content { get; set; } = string.Empty;
}
