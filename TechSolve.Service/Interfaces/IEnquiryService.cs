using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;

namespace TechSolve.Service.Interfaces;

public interface IEnquiryService
{
    Task<EnquiryResponse> CreateAsync(CreateEnquiryRequest request, string? ip, string? ua);
    Task<EnquiryResponse?> GetByTicketIdAsync(string ticketId);
    Task<PagedResponse<EnquiryResponse>> GetPagedAsync(GetEnquiriesRequest request);
}
