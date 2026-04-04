using TechSolve.Domain.Enums;

namespace TechSolve.Domain.Requests;

public class GetEnquiriesRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? ServiceSlug { get; set; }
    public EnquiryStatus? Status { get; set; }
}
