using CrpgP.Application;
using CrpgP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class PortraitEndpoints
{
    public static void MapPortraitEndpoints(this WebApplication app)
    {
        var portraitService = app.Services.GetService<PortraitService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(PortraitService)}.");
        
        
        app.MapGet("api/v1/portrait", async Task<IResult>([FromQuery(Name="id")] int id) =>
        {
            var result = await portraitService.GetPortraitByIdAsync(id);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapGet("api/v1/portraits", async Task<IResult>([FromQuery(Name="id")] int[] ids) =>
        {
            var result = await portraitService.GetPortraitsByIdsAsync(ids);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });

        app.MapGet("api/v1/portrait/all/count", async Task<IResult> () =>
        {
            var result = await portraitService.GetPortraitsCount();
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapGet("api/v1/portrait/all", async Task<IResult>(
            [FromQuery(Name="page")] int page,
            [FromQuery(Name="count")] int count) =>
        {
            var result = await portraitService.GetPortraitsPage(page, count);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });

        app.MapPost("api/v1/portrait/create", async Task<IResult>(Portrait portrait) =>
        {
            var result = await portraitService.CreatePortraitAsync(portrait);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapPut("api/v1/portrait/update", async Task<IResult>(Portrait portrait) =>
        {
            var result = await portraitService.UpdatePortraitAsync(portrait);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapDelete("api/v1/portrait/delete", async Task<IResult>([FromQuery(Name="id")] int id) =>
        {
            var result = await portraitService.DeletePortraitAsync(id);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
    }
}