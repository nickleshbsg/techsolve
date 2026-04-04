using System.ComponentModel.DataAnnotations;

namespace TechSolve.Domain.Requests;

public class CreateEnquiryRequest
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(150)]
    public string? Company { get; set; }

    [Required(ErrorMessage = "Service slug is required.")]
    [StringLength(100)]
    public string ServiceSlug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Service name is required.")]
    [StringLength(200)]
    public string ServiceName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required.")]
    [StringLength(5000, MinimumLength = 20, ErrorMessage = "Please describe your project (min 20 characters).")]
    public string Message { get; set; } = string.Empty;

    [StringLength(1000)]
    public string SourcePageUrl { get; set; } = string.Empty;

    [StringLength(300)]
    public string? SourcePageTitle { get; set; }
}
