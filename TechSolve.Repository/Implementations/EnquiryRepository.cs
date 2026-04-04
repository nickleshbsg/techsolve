using Microsoft.EntityFrameworkCore;
using TechSolve.DataModel;
using TechSolve.Domain.Entities;
using TechSolve.Domain.Enums;
using TechSolve.Domain.Interfaces;

namespace TechSolve.Repository.Implementations;

public class EnquiryRepository : GenericRepository<Enquiry>, IEnquiryRepository
{
    public EnquiryRepository(AppDbContext context) : base(context) { }

    public async Task<Enquiry?> GetByTicketIdAsync(string ticketId) =>
        await _dbSet.FirstOrDefaultAsync(e => e.TicketId == ticketId);

    public async Task<IEnumerable<Enquiry>> GetByServiceSlugAsync(string slug) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.ServiceSlug == slug)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Enquiry>> GetByStatusAsync(EnquiryStatus status) =>
        await _dbSet.AsNoTracking()
            .Where(e => e.Status == status)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Enquiry>> GetPagedAsync(int page, int pageSize) =>
        await _dbSet.AsNoTracking()
            .OrderByDescending(e => e.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
}
