using Microsoft.EntityFrameworkCore;
using TechSolve.DataModel;
using TechSolve.Domain.Entities;
using TechSolve.Domain.Interfaces;

namespace TechSolve.Repository.Implementations;

public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
{
    public BlogPostRepository(AppDbContext context) : base(context) { }

    public async Task<BlogPost?> GetBySlugAsync(string slug) =>
        await _dbSet.FirstOrDefaultAsync(e => e.Slug == slug && e.IsPublished);

    public async Task<IEnumerable<BlogPost>> GetPublishedAsync(int page, int pageSize) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.IsPublished)
            .OrderByDescending(e => e.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(string category) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.Category == category && e.IsPublished)
            .OrderByDescending(e => e.PublishedAt)
            .ToListAsync();

    public async Task IncrementViewCountAsync(int id)
    {
        await _dbSet.Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.ViewCount, e => e.ViewCount + 1));
    }
}
