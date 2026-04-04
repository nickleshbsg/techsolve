using Microsoft.EntityFrameworkCore;
using TechSolve.DataModel;
using TechSolve.Domain.Entities;
using TechSolve.Domain.Interfaces;

namespace TechSolve.Repository.Implementations;

public class ServiceRepository : GenericRepository<Domain.Entities.Service>, IServiceRepository
{
    public ServiceRepository(AppDbContext context) : base(context) { }

    public async Task<Domain.Entities.Service?> GetBySlugAsync(string slug) =>
        await _dbSet.FirstOrDefaultAsync(e => e.Slug == slug && e.IsActive);

    public async Task<IEnumerable<Domain.Entities.Service>> GetActiveAsync() =>
        await _dbSet.AsNoTracking()
            .Where(e => e.IsActive)
            .OrderBy(e => e.SortOrder)
            .ToListAsync();
}
