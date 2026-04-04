namespace TechSolve.Domain.Requests;

public class GetBlogRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 9;
    public string? Category { get; set; }
    public string? SearchTerm { get; set; }
}
