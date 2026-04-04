using TechSolve.Domain.Responses;

namespace TechSolve.Service.Interfaces;

public interface IServiceCatalogService
{
    Task<IEnumerable<ServiceSummaryResponse>> GetAllActiveAsync();
    Task<ServiceDetailResponse?> GetBySlugAsync(string slug);
}
