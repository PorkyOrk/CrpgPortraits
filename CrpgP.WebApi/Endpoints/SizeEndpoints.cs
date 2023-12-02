using CrpgP.Application;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class SizeEndpoints
{
    public static void MapSizeEndpoints(this WebApplication app)
    {
        var sizeService = app.Services.GetService<SizeService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(SizeService)}.");
        
        
        app.MapGet("api/v1/size", async Task<IResult>(
            [FromQuery(Name = "id")] int id) =>
        {
            var result = await sizeService.GetSizeByIdAsync(id);
            return Results.Json(result);
        });
        
        app.MapGet("api/v1/size/find", async Task<IResult>(
            [FromQuery(Name = "width")] int width,
            [FromQuery(Name = "height")] int height) =>
        {
            var result = await sizeService.GetSizeByDimensionsAsync(width, height);
            return Results.Json(result);
        });

        app.MapPost("api/v1/size/create", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await sizeService.CreateSizeAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapPut("api/v1/size/update", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await sizeService.UpdateSizeAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapDelete("api/v1/size/delete", async Task<IResult>(
            [FromQuery(Name = "id")] int id) =>
        {
            var result = await sizeService.DeleteSizeAsync(id);
            return Results.Json(result);
        });
    }
}