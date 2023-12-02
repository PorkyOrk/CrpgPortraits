using CrpgP.Application;
using CrpgP.Domain.Entities;

namespace CrpgP.WebApi.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var gameService = app.Services.GetService<GameService>()
            ?? throw new NullReferenceException("Unable to find GameService.");
        
        app.MapGet("api/v1/game/{id}", async Task<IResult>(int id) =>
        {
            var result = await gameService.GetGameByIdAsync(id);
            return Results.Json(result);
        });
        
        app.MapGet("api/v1/game/find/", async Task<IResult> (HttpRequest request) =>
        {
            var name = request.Query["name"].ToString();
            var result = await gameService.GetGameByNameAsync(name);
            return Results.Json(result);
        });
        
        app.MapPost("api/v1/game/create/", async Task<IResult>(HttpRequest request) =>
        {
            var game = await request.ReadFromJsonAsync<Game>();
            if (game is null)
                return Results.BadRequest("Invalid payload.");
            
            var result = await gameService.CreateGameAsync(game);
            return Results.Json(result);
        });
        
        app.MapPut("api/v1/game/update/", async Task<IResult> (HttpRequest request) =>
        {
            var game = await request.ReadFromJsonAsync<Game>();
            if (game is null)
                return Results.BadRequest("Invalid payload.");
            
            var result = await gameService.UpdateGameAsync(game);
            return Results.Json(result);
        });
        
        app.MapDelete("api/v1/game/delete/{id}", async Task<IResult>(int id) =>
        {
            var result = await gameService.DeleteGameAsync(id);
            return Results.Json(result);
        });
    }
}