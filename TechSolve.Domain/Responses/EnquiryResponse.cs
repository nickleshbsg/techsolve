using TechSolve.Domain.Enums;

namespace TechSolve.Domain.Responses;

public class EnquiryResponse
{
    public int Id { get; set; }
    public string TicketId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceSlug { get; set; } = string.Empty;
    public EnquiryStatus Status { get; set; }
    public string StatusLabel { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
