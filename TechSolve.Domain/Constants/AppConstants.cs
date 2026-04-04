namespace TechSolve.Domain.Constants;

public static class AppConstants
{
    public const string AppName = "TechSolve Consulting";
    public const string AdminEmail = "hello@techsolve.in";
    public const string SupportPhone = "+91 98765 43210";
    public const string WhatsAppNumber = "919876543210";

    public static class TicketPrefix { public const string Enquiry = "TS"; }

    public static class Pagination
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 9;
        public const int MaxPageSize = 50;
    }

    public static class Cache
    {
        public const int ServicesCacheMinutes = 60;
        public const int BlogCacheMinutes = 10;
    }

    public static class ServiceSlugs
    {
        public const string CustomSoftware = "custom-software-development";
        public const string WebApp = "web-application-development";
        public const string MobileApp = "mobile-app-development";
        public const string AiMl = "ai-machine-learning";
        public const string CloudDevOps = "cloud-devops";
        public const string Cybersecurity = "cybersecurity-compliance";
    }

    public static class EnquiryStatusLabels
    {
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string Responded = "Responded";
        public const string Closed = "Closed";
        public const string Spam = "Spam";
    }
}
