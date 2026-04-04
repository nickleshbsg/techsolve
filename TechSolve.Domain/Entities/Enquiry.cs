using TechSolve.Domain.Enums;

namespace TechSolve.Domain.Entities;

public class Enquiry : BaseEntity
{
    public string TicketId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string ServiceSlug { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string SourcePageUrl { get; set; } = string.Empty;
    public string? SourcePageTitle { get; set; }
    public EnquiryStatus Status { get; set; } = EnquiryStatus.New;
    public string? AssignedTo { get; set; }
    public string? AdminNotes { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
