using System.ComponentModel.DataAnnotations;

namespace TechSolve.Domain.Requests;

public class TrackWhatsAppRequest
{
    [Required]
    [StringLength(100)]
    public string SessionId { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string PageUrl { get; set; } = string.Empty;

    [StringLength(300)]
    public string? PageTitle { get; set; }

    [StringLength(100)]
    public string? ServiceSlug { get; set; }

    [StringLength(200)]
    public string? ServiceName { get; set; }

    [StringLength(1000)]
    public string? Referrer { get; set; }

    [Required]
    [StringLength(100)]
    public string ClickSource { get; set; } = string.Empty;
}
