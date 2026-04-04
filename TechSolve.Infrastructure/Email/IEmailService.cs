using TechSolve.Domain.Entities;

namespace TechSolve.Infrastructure.Email;

public interface IEmailService
{
    Task SendAdminNotificationAsync(Enquiry enquiry);
    Task SendClientAcknowledgementAsync(Enquiry enquiry);
}
