using System.Text.Json;
using TechSolve.Domain.Interfaces;
using TechSolve.Domain.Responses;
using TechSolve.Service.Interfaces;

namespace TechSolve.Service.Implementations;

public class ServiceCatalogService : IServiceCatalogService
{
    private readonly IServiceRepository _repo;
    public ServiceCatalogService(IServiceRepository repo) => _repo = repo;

    public async Task<IEnumerable<ServiceSummaryResponse>> GetAllActiveAsync() =>
        (await _repo.GetActiveAsync()).Select(s => new ServiceSummaryResponse
        {
            Id = s.Id,
            Slug = s.Slug,
            Title = s.Title,
            Tagline = s.Tagline,
            ShortDescription = s.ShortDescription,
            IconEmoji = s.IconEmoji
        });

    public async Task<ServiceDetailResponse?> GetBySlugAsync(string slug)
    {
        var s = await _repo.GetBySlugAsync(slug);
        if (s is null) return null;
        return new ServiceDetailResponse
        {
            Id = s.Id,
            Slug = s.Slug,
            Title = s.Title,
            Tagline = s.Tagline,
            ShortDescription = s.ShortDescription,
            LongDescription = s.LongDescription,
            IconEmoji = s.IconEmoji,
            HeroImageUrl = s.HeroImageUrl,
            KeyFeatures = s.KeyFeatures is not null
                ? JsonSerializer.Deserialize<List<string>>(s.KeyFeatures) ?? []
                : [],
            TechStack = s.TechStack is not null
                ? JsonSerializer.Deserialize<List<string>>(s.TechStack) ?? []
                : []
        };
    }
}
