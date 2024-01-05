using CrpgP.Application;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class PortraitEndpoints
{
    public static void MapPortraitEndpoints(this WebApplication app)
    {
        var portraitService = app.Services.GetService<PortraitService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(PortraitService)}.");
        
        
        app.MapGet("api/v1/portrait", async Task<IResult>(
            [FromQuery(Name="id")] int id) =>
        {
            var result = await portraitService.GetPortraitByIdAsync(id);
            return Results.Json(result);
        });
        
        app.MapGet("api/v1/portraits", async Task<IResult>(
            [FromQuery(Name="id")] int[] ids) =>
        {
            var result = await portraitService.GetPortraitsByIdsAsync(ids);
            return Results.Json(result);
        });

        app.MapPost("api/v1/portrait/create", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await portraitService.CreatePortraitAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapPut("api/v1/portrait/update", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await portraitService.UpdatePortraitAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapDelete("api/v1/portrait/delete", async Task<IResult>(
            [FromQuery(Name="id")] int id) =>
        {
            var result = await portraitService.DeletePortraitAsync(id);
            return Results.Json(result);
        });
    }
}