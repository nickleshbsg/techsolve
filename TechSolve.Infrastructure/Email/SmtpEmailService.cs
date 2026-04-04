using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using TechSolve.Domain.Entities;

namespace TechSolve.Infrastructure.Email;

public class SmtpEmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IOptions<EmailSettings> opts, ILogger<SmtpEmailService> logger)
    {
        _settings = opts.Value;
        _logger = logger;
    }

    public async Task SendAdminNotificationAsync(Enquiry enquiry)
    {
        var body = $"""
            <h2>New Enquiry — Ticket {enquiry.TicketId}</h2>
            <table cellpadding="8" style="border-collapse:collapse;font-family:sans-serif">
              <tr><td><b>Name</b></td><td>{enquiry.FullName}</td></tr>
              <tr><td><b>Email</b></td><td>{enquiry.Email}</td></tr>
              <tr><td><b>Phone</b></td><td>{enquiry.Phone ?? "—"}</td></tr>
              <tr><td><b>Company</b></td><td>{enquiry.Company ?? "—"}</td></tr>
              <tr><td><b>Service</b></td><td>{enquiry.ServiceName}</td></tr>
              <tr><td><b>Source</b></td><td>{enquiry.SourcePageUrl}</td></tr>
              <tr><td><b>Message</b></td><td>{enquiry.Message}</td></tr>
            </table>
            """;
        await SendAsync(_settings.AdminEmail, $"[TechSolve] New Enquiry: {enquiry.TicketId}", body);
    }

    public async Task SendClientAcknowledgementAsync(Enquiry enquiry)
    {
        var body = $"""
            <p>Dear {enquiry.FullName},</p>
            <p>Thank you for reaching out to TechSolve Consulting. We have received your enquiry 
            regarding <b>{enquiry.ServiceName}</b> and a senior consultant will respond within 24 hours.</p>
            <p><b>Your ticket ID: {enquiry.TicketId}</b></p>
            <p>If you have additional questions, you can reply to this email or WhatsApp us at +91 98765 43210.</p>
            <p>Warm regards,<br/>TechSolve Consulting Team</p>
            """;
        await SendAsync(enquiry.Email, $"We've received your enquiry — {enquiry.TicketId}", body);
    }

    private async Task SendAsync(string to, string subject, string htmlBody)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To} with subject {Subject}", to, subject);
        }
    }
}
