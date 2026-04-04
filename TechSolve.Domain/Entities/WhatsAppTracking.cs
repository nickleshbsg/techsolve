namespace TechSolve.Domain.Entities;

public class WhatsAppTracking : BaseEntity
{
    public string SessionId { get; set; } = string.Empty;
    public string PageUrl { get; set; } = string.Empty;
    public string? PageTitle { get; set; }
    public string? ServiceSlug { get; set; }
    public string? ServiceName { get; set; }
    public string? Referrer { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string ClickSource { get; set; } = string.Empty;
}
