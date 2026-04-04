using TechSolve.Domain.Entities;

namespace TechSolve.Domain.Interfaces;

public interface IBlogPostRepository : IRepository<BlogPost>
{
    Task<BlogPost?> GetBySlugAsync(string slug);
    Task<IEnumerable<BlogPost>> GetPublishedAsync(int page, int pageSize);
    Task<IEnumerable<BlogPost>> GetByCategoryAsync(string category);
    Task IncrementViewCountAsync(int id);
}
