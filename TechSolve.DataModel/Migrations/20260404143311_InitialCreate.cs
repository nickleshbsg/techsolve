using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechSolve.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slug = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Excerpt = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AuthorName = table.Column<string>(type: "TEXT", nullable: false),
                    ReadTimeMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ViewCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketId = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Company = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    ServiceSlug = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ServiceName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    SourcePageUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    SourcePageTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedTo = table.Column<string>(type: "TEXT", nullable: true),
                    AdminNotes = table.Column<string>(type: "TEXT", nullable: true),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Tagline = table.Column<string>(type: "TEXT", nullable: false),
                    ShortDescription = table.Column<string>(type: "TEXT", nullable: false),
                    LongDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IconEmoji = table.Column<string>(type: "TEXT", nullable: false),
                    HeroImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    KeyFeatures = table.Column<string>(type: "TEXT", nullable: true),
                    TechStack = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhatsAppTrackings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PageUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    PageTitle = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceSlug = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ServiceName = table.Column<string>(type: "TEXT", nullable: true),
                    Referrer = table.Column<string>(type: "TEXT", nullable: true),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    ClickSource = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhatsAppTrackings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "AuthorName", "Category", "Content", "CoverImageUrl", "CreatedAt", "Excerpt", "IsDeleted", "IsPublished", "PublishedAt", "ReadTimeMinutes", "Slug", "Title", "UpdatedAt", "ViewCount" },
                values: new object[,]
                {
                    { 1, "Ravi Kumar", "Angular & .NET Core", "<h2>Why Signals change everything</h2><p>Angular 19's signals system fundamentally shifts how we think about reactivity in large applications. Instead of observables that can be complex to manage, signals offer a simpler, more predictable model.</p><h2>Key benefits for enterprise</h2><p>For enterprise applications, signals bring three key advantages: predictable change detection, simpler debugging, and better performance out of the box.</p>", null, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Signals replace RxJS for most state management needs in Angular 19. Here's what that means for large-scale applications.", false, true, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "angular-19-signals-complete-guide", "Angular 19 Signals: The Complete Guide for Enterprise Apps", null, 2341 },
                    { 2, "Priya Nair", "Angular & .NET Core", "<h2>The false dichotomy</h2><p>The .NET community often presents this as a binary choice. It isn't. In practice, both approaches coexist in the same application depending on the use case.</p><h2>When Controllers still win</h2><p>For complex APIs with action filters, model validation attributes, and rich Swagger documentation, controllers remain more expressive.</p>", null, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minimal APIs are not a replacement for controllers in enterprise applications. Here's the nuanced decision framework we use at TechSolve.", false, true, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "dotnet-9-minimal-api-vs-controllers", ".NET 9 Minimal APIs vs Controllers: When to Use Which", null, 1876 },
                    { 3, "Karan Mehta", "AI & Machine Learning", "<h2>The problem</h2><p>Our client — a top-5 Indian insurance company — was processing 50,000 policy documents per day manually. Turnaround time was 48 hours. Error rate was 4.2%. The business needed sub-2-hour processing with under 0.1% errors.</p><h2>The results</h2><p>After 90 days in production: average processing time dropped to 23 minutes, error rate fell to 0.08%, and the client reallocated 34 full-time employees to higher-value work.</p>", null, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A practical walkthrough of the architecture behind a production document intelligence system built for a leading Indian insurer.", false, true, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "ai-document-processing-bfsi", "How We Built an AI Document Pipeline Processing 50,000 Pages Per Day", null, 4120 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CreatedAt", "HeroImageUrl", "IconEmoji", "IsActive", "IsDeleted", "KeyFeatures", "LongDescription", "ShortDescription", "Slug", "SortOrder", "Tagline", "TechStack", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "⚙️", true, false, "[\"Requirements analysis & system design\",\"Agile delivery with 2-week sprints\",\"Code review & quality gates\",\"Full test coverage (unit + integration)\",\"Deployment & handover support\",\"3-month post-launch warranty\"]", "We design and build software that fits your business precisely — not a generic product forced to adapt. Our senior engineers work with you to map processes, architect solutions, and deliver production-grade software on time. Every engagement includes full documentation, NDA, and IP transfer.", "Bespoke applications engineered for your unique business processes — from complex enterprise workflows to greenfield SaaS platforms.", "custom-software-development", 1, "Built for your exact requirements", "[\"Angular 19\",\".NET 9\",\"SQL Server\",\"Azure\",\"Docker\",\"GitHub Actions\"]", "Custom Software Development", null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🌐", true, false, "[\"Angular 19 standalone components\",\"Progressive Web App support\",\"REST & GraphQL APIs\",\"Role-based access control\",\"Real-time features (SignalR)\",\"CI/CD pipeline setup\"]", "From customer portals to internal dashboards, we build web applications that perform at scale. Our Angular + .NET stack delivers type-safe, maintainable code. All apps are built mobile-first, accessibility-compliant, and optimised for Core Web Vitals.", "Modern web applications built with Angular 19 and .NET Core 9 — designed for performance, accessibility, and long-term maintainability.", "web-application-development", 2, "Fast, scalable, production-ready", "[\"Angular 19\",\"TypeScript\",\".NET 9\",\"SignalR\",\"Redis\",\"PostgreSQL\"]", "Web Application Development", null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🤖", true, false, "[\"Document intelligence & OCR\",\"Predictive analytics models\",\"NLP & text classification\",\"Computer vision pipelines\",\"Model monitoring & retraining\",\"Integration with existing systems\"]", "We build practical AI that delivers measurable ROI, not demos. From document processing pipelines handling 10,000+ pages per day to recommendation engines and predictive maintenance systems.", "Custom AI/ML solutions — document intelligence, predictive analytics, NLP, and computer vision — integrated into your existing systems.", "ai-machine-learning", 3, "Intelligence built into your product", "[\"Python\",\"FastAPI\",\"TensorFlow\",\"PyTorch\",\"Azure ML\",\"OpenAI API\"]", "AI & Machine Learning", null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "📱", true, false, "[\"Flutter & React Native\",\"Offline-first architecture\",\"Push notifications\",\"Biometric authentication\",\"App Store & Play Store submission\",\"Analytics & crash reporting\"]", "We deliver mobile apps that users love. Cross-platform approach means fully native iOS and Android apps from a single codebase. App Store and Play Store submission handled end-to-end.", "Cross-platform mobile apps built with Flutter or React Native — native performance, beautiful UI, single team for both platforms.", "mobile-app-development", 4, "iOS & Android from a single codebase", "[\"Flutter\",\"React Native\",\"Firebase\",\"REST APIs\",\".NET Backend\",\"SQLite\"]", "Mobile App Development", null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "☁️", true, false, "[\"Cloud migration (Azure/AWS/GCP)\",\"Kubernetes & Docker orchestration\",\"CI/CD pipeline design\",\"Infrastructure as Code (Terraform)\",\"Cost optimisation\",\"24x7 managed infrastructure\"]", "We help engineering teams move fast without breaking things. From cloud migrations to Kubernetes clusters and fully automated CI/CD pipelines, we bring DevOps maturity to your organisation.", "Cloud migration, infrastructure automation, and DevOps transformation on Azure, AWS, or GCP.", "cloud-devops", 5, "99.99% uptime, zero compromise", "[\"Azure\",\"AWS\",\"Kubernetes\",\"Terraform\",\"Docker\",\"GitHub Actions\"]", "Cloud & DevOps", null },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🛡️", true, false, "[\"Security audit & gap analysis\",\"Penetration testing\",\"ISO 27001 implementation\",\"GDPR & RBI compliance\",\"Security awareness training\",\"Incident response planning\"]", "Our cybersecurity practice helps organisations achieve and maintain compliance with ISO 27001, GDPR, RBI guidelines, and SEBI requirements. We combine technical security assessments with policy consulting.", "Security audits, penetration testing, and compliance implementation for BFSI and enterprise clients.", "cybersecurity-compliance", 6, "ISO 27001 · GDPR · RBI compliant", "[\"SIEM tools\",\"Kali Linux\",\"Burp Suite\",\"AWS Security Hub\",\"Azure Defender\",\"OWASP ZAP\"]", "Cybersecurity & Compliance", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_Category",
                table: "BlogPosts",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_IsPublished",
                table: "BlogPosts",
                column: "IsPublished");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_Slug",
                table: "BlogPosts",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_Email",
                table: "Enquiries",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_ServiceSlug",
                table: "Enquiries",
                column: "ServiceSlug");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_Status",
                table: "Enquiries",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_TicketId",
                table: "Enquiries",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WhatsAppTrackings_CreatedAt",
                table: "WhatsAppTrackings",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WhatsAppTrackings_ServiceSlug",
                table: "WhatsAppTrackings",
                column: "ServiceSlug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Enquiries");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "WhatsAppTrackings");
        }
    }
}
