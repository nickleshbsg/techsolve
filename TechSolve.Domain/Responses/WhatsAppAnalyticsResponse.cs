namespace TechSolve.Domain.Responses;

public class WhatsAppAnalyticsResponse
{
    public int TotalClicks { get; set; }
    public Dictionary<string, int> ClicksByService { get; set; } = [];
    public Dictionary<string, int> ClicksBySource { get; set; } = [];
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
