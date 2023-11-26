using CrpgP.Application;

namespace CrpgP.WebApi.Endpoints;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this WebApplication app)
    {
        var tagService = app.Services.GetService<TagService>()
                          ?? throw new NullReferenceException("Unable to find Tag Service.");
        
        app.MapGet("api/v1/tag/{id}", async Task<IResult>(int id) =>
        {
            // Not implemented
            return Results.NotFound();
        });

        app.MapPost("api/v1/tag/create/", async Task<IResult>(HttpRequest request) =>
        {
            // Not implemented
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            return Results.NotFound();
        });
        
        app.MapPut("api/v1/tag/update/", async Task<IResult> (HttpContext context) =>
        {
            // Not implemented
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            return Results.NotFound();
        });
        
        app.MapDelete("api/v1/tag/delete/{id}", async Task<IResult>(int id) =>
        {
            // Not implemented
            return Results.NotFound();
        });
    }
}