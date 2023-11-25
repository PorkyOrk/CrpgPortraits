namespace CrpgP.WebApi.Endpoints;

public static class EndpointsSetup
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGameEndpoints();
        app.MapPortraitEndpoints();
        app.MapSizeEndpoints();
        app.MapTagEndpoints();
        
        // ********************
        // Health Endpoints 
        // ********************
        app.MapGet("api/v1/health",() => "Healthy");
        app.MapGet("/api/v1/health/db", () => "Database Health Check");

        app.MapHealthChecks("/healthz");
        
        // =====================================================
        
        
        
        
        
        // ********************
        //Example Endpoints 
        // ********************
        app.MapGet("/",() => "Hello World");
        app.MapGet("/myendpoint", () => "This is my endpoint!");
        
        // Does not show in swagger 
        app.MapGet("/endone", async context =>
        {
            await context.Response.WriteAsJsonAsync(new { Message = "This is endpoint one" });
        });
        // Does not show in swagger
        app.MapGet("/endtwo", async context =>
        {
            await context.Response.WriteAsJsonAsync(new { Message = "This is endpoint two" });
        });
        // =====================================================
        
    }
}