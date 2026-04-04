using TechSolve.Domain.Enums;

namespace TechSolve.Domain.DTOs;

/// <summary>Internal DTO used for service-to-service mapping. Not exposed via API.</summary>
public class EnquiryDto
{
    public int Id { get; set; }
    public string TicketId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string ServiceSlug { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string SourcePageUrl { get; set; } = string.Empty;
    public EnquiryStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
