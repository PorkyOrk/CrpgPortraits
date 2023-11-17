using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CrpgP.Presentation;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        
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
        
    }
}