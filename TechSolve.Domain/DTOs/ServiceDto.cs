namespace TechSolve.Domain.DTOs;

public class ServiceDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = string.Empty;
    public string? HeroImageUrl { get; set; }
    public string? KeyFeaturesJson { get; set; }
    public string? TechStackJson { get; set; }
    public int SortOrder { get; set; }
}
