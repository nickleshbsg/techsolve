namespace TechSolve.Domain.Entities;

public class Service : BaseEntity
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = string.Empty;
    public string? HeroImageUrl { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public string? KeyFeatures { get; set; }   // JSON serialised
    public string? TechStack { get; set; }     // JSON serialised
}
