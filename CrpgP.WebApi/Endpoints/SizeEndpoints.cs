using CrpgP.Application;
using CrpgP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class SizeEndpoints
{
    public static void MapSizeEndpoints(this WebApplication app)
    {
        var sizeService = app.Services.GetService<SizeService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(SizeService)}.");
        
        
        app.MapGet("api/v1/size", async Task<IResult>([FromQuery(Name = "id")] int id) =>
        {
            var result = await sizeService.GetSizeByIdAsync(id);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapGet("api/v1/size/find", async Task<IResult>(
            [FromQuery(Name = "width")] int width, [FromQuery(Name = "height")] int height) =>
        {
            var result = await sizeService.GetSizeByDimensionsAsync(width, height);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });

        app.MapPost("api/v1/size/create", async Task<IResult>(Size size) =>
        {
            var result = await sizeService.CreateSizeAsync(size);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapPut("api/v1/size/update", async Task<IResult>(Size size) =>
        {
            var result = await sizeService.UpdateSizeAsync(size);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapDelete("api/v1/size/delete", async Task<IResult>([FromQuery(Name = "id")] int id) =>
        {
            var result = await sizeService.DeleteSizeAsync(id);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
    }
}