using TechSolve.Domain.Entities;
using TechSolve.Domain.Enums;

namespace TechSolve.Domain.Interfaces;

public interface IEnquiryRepository : IRepository<Enquiry>
{
    Task<Enquiry?> GetByTicketIdAsync(string ticketId);
    Task<IEnumerable<Enquiry>> GetByServiceSlugAsync(string slug);
    Task<IEnumerable<Enquiry>> GetByStatusAsync(EnquiryStatus status);
    Task<IEnumerable<Enquiry>> GetPagedAsync(int page, int pageSize);
}
