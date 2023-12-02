using CrpgP.Application;
using Microsoft.AspNetCore.Mvc;

namespace CrpgP.WebApi.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var gameService = app.Services.GetService<GameService>()
            ?? throw new NullReferenceException($"Unable to find {nameof(TagService)}.");
        
        
        app.MapGet("api/v1/game", async Task<IResult>(
            [FromQuery(Name="id")] int id) =>
        {
            var result = await gameService.GetGameByIdAsync(id);
            return Results.Json(result);
        });
        
        app.MapGet("api/v1/game/find", async Task<IResult>(
            [FromQuery(Name="name")] string name) =>
        {
            var result = await gameService.GetGameByNameAsync(name);
            return Results.Json(result);
        });
        
        app.MapPost("api/v1/game/create", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await gameService.CreateGameAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapPut("api/v1/game/update", async Task<IResult>(
            HttpRequest request) =>
        {
            var jsonBody = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await gameService.UpdateGameAsync(jsonBody);
            return Results.Json(result);
        });
        
        app.MapDelete("api/v1/game/delete", async Task<IResult>(
            [FromQuery(Name="id")] int id) =>
        {
            var result = await gameService.DeleteGameAsync(id);
            return Results.Json(result);
        });
    }
}