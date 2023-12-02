using CrpgP.Application;

namespace CrpgP.WebApi.Endpoints;

public static class SizeEndpoints
{
    public static void MapSizeEndpoints(this WebApplication app)
    {
        var sizeService = app.Services.GetService<SizeService>()
            ?? throw new NullReferenceException("Unable to find Size Service.");
        
        app.MapGet("api/v1/size/{id}", async Task<IResult>(int id) =>
        {
            // Not implemented
            return Results.NotFound();
        });

        app.MapPost("api/v1/size/create/", async Task<IResult>(HttpRequest request) =>
        {
            // Not implemented
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            return Results.NotFound();
        });
        
        app.MapPut("api/v1/size/update/", async Task<IResult> (HttpContext context) =>
        {
            // Not implemented
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            return Results.NotFound();
        });
        
        app.MapDelete("api/v1/size/delete/{id}", async Task<IResult>(int id) =>
        {
            // Not implemented
            return Results.NotFound();
        });
    }
}