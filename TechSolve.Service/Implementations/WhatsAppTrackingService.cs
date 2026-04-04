using TechSolve.Domain.Entities;
using TechSolve.Domain.Interfaces;
using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;
using TechSolve.Service.Interfaces;

namespace TechSolve.Service.Implementations;

public class WhatsAppTrackingService : IWhatsAppTrackingService
{
    private readonly IWhatsAppTrackingRepository _repo;
    public WhatsAppTrackingService(IWhatsAppTrackingRepository repo) => _repo = repo;

    public async Task TrackClickAsync(TrackWhatsAppRequest req, string? ip, string? ua)
    {
        await _repo.AddAsync(new WhatsAppTracking
        {
            SessionId = req.SessionId,
            PageUrl = req.PageUrl,
            PageTitle = req.PageTitle,
            ServiceSlug = req.ServiceSlug,
            ServiceName = req.ServiceName,
            Referrer = req.Referrer,
            ClickSource = req.ClickSource,
            IpAddress = ip,
            UserAgent = ua
        });
    }

    public async Task<WhatsAppAnalyticsResponse> GetAnalyticsAsync(DateTime from, DateTime to)
    {
        var all = await _repo.GetByDateRangeAsync(from, to);
        var byService = await _repo.GetClicksByServiceAsync(from, to);
        var bySource = await _repo.GetClicksBySourceAsync(from, to);
        return new WhatsAppAnalyticsResponse
        {
            TotalClicks = all.Count(),
            ClicksByService = byService,
            ClicksBySource = bySource,
            From = from,
            To = to
        };
    }
}
