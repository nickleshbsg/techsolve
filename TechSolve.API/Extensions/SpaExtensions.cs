namespace TechSolve.API.Extensions;

/// <summary>
/// Configures how the Angular frontend is served.
///
/// DEVELOPMENT
///   The API only handles /api/* routes.
///   Angular runs separately via its own dev server (ng serve → localhost:4200).
///   Open two terminals: one for `dotnet run`, one for `npm start`.
///   No proxying needed — CORS is already configured to allow localhost:4200.
///
/// PRODUCTION
///   `dotnet publish` triggers ng build via TechSolve.UI.csproj PublishRunWebpack target.
///   The built Angular files land in TechSolve.API/wwwroot.
///   The API serves them as static files with an index.html fallback for
///   Angular's client-side routing (all non-/api routes return index.html).
/// </summary>
public static class SpaExtensions
{
    public static WebApplication UseSpaWithAngular(this WebApplication app, IConfiguration config)
    {
        if (app.Environment.IsDevelopment())
        {
            // ── DEVELOPMENT ────────────────────────────────────────────────────
            // Angular runs on its own dev server (npm start → localhost:4200).
            // This API handles only /api/* — no static file serving needed here.
            // CORS in ServiceExtensions already allows http://localhost:4200.
        }
        else
        {
            // ── PRODUCTION ─────────────────────────────────────────────────────
            // Serve pre-built Angular dist from wwwroot (placed there by publish).
            app.UseDefaultFiles();               // maps / → /index.html
            app.UseStaticFiles();                // serves JS, CSS, assets from wwwroot
            app.MapFallbackToFile("index.html"); // client-side routing fallback
        }

        return app;
    }
}
