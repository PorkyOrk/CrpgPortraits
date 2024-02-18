using System.Text.Json;
using CrpgP.Application;
using CrpgP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var gameService = app.Services.GetService<GameService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(GameService)}.");
        
        
        app.MapGet("api/v1/game", async Task<IResult>([FromQuery(Name="id")] int id) =>
        {
            var result =  await gameService.GetGameByIdAsync(id);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapGet("api/v1/game/ids", async Task<IResult>() =>
        {
            var result =  await gameService.GetAllIds();
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapGet("api/v1/game/find", async Task<IResult>([FromQuery(Name="name")] string name) =>
        {
            var result = await gameService.GetGameByNameAsync(name);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapPost("api/v1/game/create", async Task<IResult>(Game game) =>
        {
            var result = await gameService.CreateGameAsync(game);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapPut("api/v1/game/update", async Task<IResult>(Game game) =>
        {
            var result = await gameService.UpdateGameAsync(game);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
        
        app.MapDelete("api/v1/game/delete", async Task<IResult>([FromQuery(Name="id")] int id) =>
        {
            var result = await gameService.DeleteGameAsync(id);
            return result.IsSuccess ? Results.Ok(result) : result.ToProblemDetails();
        });
    }
}
