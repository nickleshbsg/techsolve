using TechSolve.Domain.Constants;
using TechSolve.Domain.Entities;
using TechSolve.Domain.Enums;
using TechSolve.Domain.Interfaces;
using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;
using TechSolve.Infrastructure.Email;
using TechSolve.Service.Interfaces;

namespace TechSolve.Service.Implementations;

public class EnquiryService : IEnquiryService
{
    private readonly IEnquiryRepository _repo;
    private readonly IEmailService _email;

    public EnquiryService(IEnquiryRepository repo, IEmailService email)
    { _repo = repo; _email = email; }

    public async Task<EnquiryResponse> CreateAsync(CreateEnquiryRequest req, string? ip, string? ua)
    {
        var entity = new Enquiry
        {
            TicketId = GenerateTicketId(),
            FullName = req.FullName,
            Email = req.Email,
            Phone = req.Phone,
            Company = req.Company,
            ServiceSlug = req.ServiceSlug,
            ServiceName = req.ServiceName,
            Message = req.Message,
            SourcePageUrl = req.SourcePageUrl,
            SourcePageTitle = req.SourcePageTitle,
            IpAddress = ip,
            UserAgent = ua
        };

        await _repo.AddAsync(entity);

        _ = Task.Run(async () =>
        {
            await _email.SendAdminNotificationAsync(entity);
            await _email.SendClientAcknowledgementAsync(entity);
        });

        return ToResponse(entity);
    }

    public async Task<EnquiryResponse?> GetByTicketIdAsync(string ticketId)
    {
        var e = await _repo.GetByTicketIdAsync(ticketId);
        return e is null ? null : ToResponse(e);
    }

    public async Task<PagedResponse<EnquiryResponse>> GetPagedAsync(GetEnquiriesRequest req)
    {
        IEnumerable<Enquiry> items = req.ServiceSlug is not null
            ? await _repo.GetByServiceSlugAsync(req.ServiceSlug)
            : await _repo.GetPagedAsync(req.Page, req.PageSize);

        var total = await _repo.CountAsync();
        return new PagedResponse<EnquiryResponse>
        {
            Items = items.Select(ToResponse),
            TotalCount = total,
            Page = req.Page,
            PageSize = req.PageSize
        };
    }

    private static string GenerateTicketId() =>
        $"{AppConstants.TicketPrefix.Enquiry}-{DateTime.UtcNow:yyyyMM}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

    private static EnquiryResponse ToResponse(Enquiry e) => new()
    {
        Id = e.Id,
        TicketId = e.TicketId,
        FullName = e.FullName,
        Email = e.Email,
        Phone = e.Phone,
        Company = e.Company,
        ServiceName = e.ServiceName,
        ServiceSlug = e.ServiceSlug,
        Status = e.Status,
        StatusLabel = e.Status switch
        {
            EnquiryStatus.New => AppConstants.EnquiryStatusLabels.New,
            EnquiryStatus.InProgress => AppConstants.EnquiryStatusLabels.InProgress,
            EnquiryStatus.Responded => AppConstants.EnquiryStatusLabels.Responded,
            EnquiryStatus.Closed => AppConstants.EnquiryStatusLabels.Closed,
            _ => AppConstants.EnquiryStatusLabels.Spam
        },
        CreatedAt = e.CreatedAt
    };
}
