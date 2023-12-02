using CrpgP.Application;

namespace CrpgP.WebApi.Endpoints;

public static class PortraitEndpoints
{
    public static void MapPortraitEndpoints(this WebApplication app)
    {
        var portraitService = app.Services.GetService<PortraitService>()
            ?? throw new NullReferenceException("Unable to find Portrait Service.");
        
        app.MapGet("api/v1/portrait/{id}", async Task<IResult>(int id) =>
        {
            // Not implemented
            return Results.NotFound();
        });

        app.MapPost("api/v1/portrait/create/", async Task<IResult>(HttpRequest request) =>
        {
            // Not implemented
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            return Results.NotFound();
        });
        
        app.MapPut("api/v1/portrait/update/", async Task<IResult> (HttpContext context) =>
        {
            // Not implemented
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            return Results.NotFound();
        });
        
        app.MapDelete("api/v1/portrait/delete/{id}", async Task<IResult>(int id) =>
        {
            // Not implemented
            return Results.NotFound();
        });
    }
}