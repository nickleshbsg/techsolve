using Microsoft.EntityFrameworkCore;
using TechSolve.DataModel;
using TechSolve.Domain.Entities;
using TechSolve.Domain.Interfaces;

namespace TechSolve.Repository.Implementations;

public class WhatsAppTrackingRepository : GenericRepository<WhatsAppTracking>, IWhatsAppTrackingRepository
{
    public WhatsAppTrackingRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<WhatsAppTracking>> GetByDateRangeAsync(DateTime from, DateTime to) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.CreatedAt >= from && e.CreatedAt <= to)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

    public async Task<Dictionary<string, int>> GetClicksByServiceAsync(DateTime from, DateTime to) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.CreatedAt >= from && e.CreatedAt <= to && e.ServiceSlug != null)
            .GroupBy(e => e.ServiceSlug!)
            .ToDictionaryAsync(g => g.Key, g => g.Count());

    public async Task<Dictionary<string, int>> GetClicksBySourceAsync(DateTime from, DateTime to) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.CreatedAt >= from && e.CreatedAt <= to)
            .GroupBy(e => e.ClickSource)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
}
