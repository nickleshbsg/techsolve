namespace TechSolve.Domain.Responses;

public class ServiceSummaryResponse
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = string.Empty;
}

public class ServiceDetailResponse : ServiceSummaryResponse
{
    public string LongDescription { get; set; } = string.Empty;
    public string? HeroImageUrl { get; set; }
    public List<string> KeyFeatures { get; set; } = [];
    public List<string> TechStack { get; set; } = [];
}
