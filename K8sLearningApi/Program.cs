var builder = WebApplication.CreateBuilder(args);

// Add health check services
builder.Services.AddHealthChecks();

var app = builder.Build();

// Basic endpoint
app.MapGet("/", () => "Hello World! K8s Learning API is running.");

// Liveness probe - indicates if the container is alive
app.MapGet("/health/live", () => Results.Ok(new { status = "alive", timestamp = DateTime.UtcNow }))
    .WithTags("Health");

// Readiness probe - indicates if the container is ready to accept traffic
app.MapGet("/health/ready", async () =>
{
    // In a real application, you would check dependencies here
    // (database, external services, etc.)
    var isReady = true; // Placeholder for readiness logic
    
    if (isReady)
    {
        return Results.Ok(new { status = "ready", timestamp = DateTime.UtcNow });
    }
    return Results.StatusCode(503);
})
.WithTags("Health");

// Standard health check endpoint (uses ASP.NET Core health checks)
app.MapHealthChecks("/health");

app.Run();
