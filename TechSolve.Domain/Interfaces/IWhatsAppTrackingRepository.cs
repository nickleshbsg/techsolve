using TechSolve.Domain.Entities;

namespace TechSolve.Domain.Interfaces;

public interface IWhatsAppTrackingRepository : IRepository<WhatsAppTracking>
{
    Task<IEnumerable<WhatsAppTracking>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<Dictionary<string, int>> GetClicksByServiceAsync(DateTime from, DateTime to);
    Task<Dictionary<string, int>> GetClicksBySourceAsync(DateTime from, DateTime to);
}
