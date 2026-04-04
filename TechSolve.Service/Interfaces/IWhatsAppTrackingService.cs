using TechSolve.Domain.Requests;
using TechSolve.Domain.Responses;

namespace TechSolve.Service.Interfaces;

public interface IWhatsAppTrackingService
{
    Task TrackClickAsync(TrackWhatsAppRequest request, string? ip, string? ua);
    Task<WhatsAppAnalyticsResponse> GetAnalyticsAsync(DateTime from, DateTime to);
}
