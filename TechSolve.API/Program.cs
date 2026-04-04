using Microsoft.EntityFrameworkCore;
using Serilog;
using TechSolve.API.Extensions;
using TechSolve.API.Middleware;
using TechSolve.DataModel;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .WriteTo.Console());

    builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy =
                System.Text.Json.JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            opt.JsonSerializerOptions.WriteIndented = true; // Makes debugging easier
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocs();
    builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);
    builder.Services.AddCorsPolicy(builder.Configuration);

    var app = builder.Build();

    // Auto-migrate on startup (creates DB + tables + seed data on first run)
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        var connection = db.Database.GetDbConnection();
        Log.Information($"DB Path: {connection.DataSource}");
        Log.Information("Database migration complete.");
    }

    app.UseMiddleware<ExceptionMiddleware>();

    // Swagger available in all environments for demo convenience
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechSolve API v1");
        c.RoutePrefix = "swagger";
    });
    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }
    app.UseRouting();
    app.UseCors("TechSolveCors");
    app.UseAuthorization();

    // API controllers — always mapped first so /api/* is never caught by SPA fallback
    app.MapControllers();

    // SPA static files (production only) or dev comments (see SpaExtensions.cs)
    app.UseSpaWithAngular(builder.Configuration);

    Log.Information("TechSolve API starting [{Env}]", app.Environment.EnvironmentName);
    app.Run();
}
catch (Exception ex) { Log.Fatal(ex, "Host terminated unexpectedly."); }
finally { Log.CloseAndFlush(); }
