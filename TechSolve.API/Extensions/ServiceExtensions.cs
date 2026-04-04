using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using TechSolve.DataModel;
using TechSolve.Domain.Interfaces;
using TechSolve.Infrastructure.Email;
using TechSolve.Repository.Implementations;
using TechSolve.Service.Implementations;
using TechSolve.Service.Interfaces;

namespace TechSolve.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        // Production / Render free tier → SQLite (no SQL Server needed)
        // Development / local           → SQL Server
        if (env.IsProduction() || config.GetValue<bool>("UseSqlite"))
        {
            // var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "techsolve.db");
            // services.AddDbContext<AppDbContext>(opt =>
            //     opt.UseSqlite($"Data Source={dbPath}",
            //         b => b.MigrationsAssembly("TechSolve.DataModel"))
            //        .ConfigureWarnings(w =>
            //            w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(
            config.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("TechSolve.DataModel") // Explicitly point to the project with the 'Migrations' folder
        ));
            Log.Information(env.IsProduction()
        ? "Using SQLite database for production environment."
        : "Using SQLite database as configured (UseSqlite=true).");
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(
            config.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("TechSolve.DataModel") // Explicitly point to the project with the 'Migrations' folder
        ));
        }

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IEnquiryRepository, EnquiryRepository>();
        services.AddScoped<IWhatsAppTrackingRepository, WhatsAppTrackingRepository>();
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        services.AddScoped<IEnquiryService, EnquiryService>();
        services.AddScoped<IWhatsAppTrackingService, WhatsAppTrackingService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IServiceCatalogService, ServiceCatalogService>();

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, SmtpEmailService>();

        return services;
    }

    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TechSolve Consulting API",
                Version = "v1",
                Description = "Backend API for TechSolve consulting website.",
                Contact = new OpenApiContact { Name = "TechSolve", Email = "hello@techsolve.in" }
            });
        });
        return services;
    }

    public static IServiceCollection AddCorsPolicy(
        this IServiceCollection services, IConfiguration config)
    {
        var origins = config.GetSection("AllowedOrigins").Get<string[]>()
                      ?? ["http://localhost:4200"];

        services.AddCors(opt => opt.AddPolicy("TechSolveCors", policy =>
            policy.WithOrigins(origins)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  //   .AllowCredentials()
                  ));


        return services;
    }
}
