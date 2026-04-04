using TechSolve.Domain.Entities;

namespace TechSolve.Domain.Interfaces;

public interface IServiceRepository : IRepository<Service>
{
    Task<Service?> GetBySlugAsync(string slug);
    Task<IEnumerable<Service>> GetActiveAsync();
}
