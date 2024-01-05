using CrpgP.Application;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this WebApplication app)
    {
        var tagService = app.Services.GetService<TagService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(TagService)}.");
        
        
        app.MapGet("api/v1/tag", async Task<IResult>(
            [FromQuery(Name = "id")] int id) =>
        {
            var result = await tagService.GetTagByIdAsync(id);
            return Results.Json(result);
        });
        
        app.MapGet("api/v1/tag/find", async Task<IResult>(
            [FromQuery(Name = "name")] string name) =>
        {
            var result = await tagService.GetTagByNameAsync(name);
            return Results.Json(result);
        });

        app.MapPost("api/v1/tag/create", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody =  await new StreamReader(request.Body).ReadToEndAsync();
            var result = await tagService.CreateTagAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapDelete("api/v1/tag/delete", async Task<IResult>(
            [FromQuery(Name = "id")] int id) =>
        {
            var result = await tagService.DeleteTagAsync(id);
            return Results.Json(result);
        });
    }
}