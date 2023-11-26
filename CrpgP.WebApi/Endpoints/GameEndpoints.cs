﻿using CrpgP.Application;

namespace CrpgP.WebApi.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var handler = ActivatorUtilities.CreateInstance<GameService>(app.Services);
        
        app.MapGet("api/v1/game/{id}", async Task<IResult>(int id) =>
        {
            var result = await handler.GetGameByIdAsync(id);
            return Results.Json(result);
        });
        
        app.MapGet("api/v1/game/find/", async Task<IResult> (HttpRequest request) =>
        {
            var name = request.Query["name"].ToString();
            var result = await handler.GetGameByNameAsync(name);
            return Results.Json(result);
        });
        
        app.MapPost("api/v1/game/create/", async Task<IResult>(HttpRequest request) =>
        {
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            var result = await handler.CreateGameAsync(body);
            return Results.Json(result);
        });
        
        app.MapPut("api/v1/game/update/", async Task<IResult> (HttpContext context) =>
        {
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            var result = await handler.UpdateGameAsync(body);
            return Results.Json(result);
        });
        
        app.MapDelete("api/v1/game/delete/{id}", async Task<IResult>(int id) =>
        {
            var result = await handler.DeleteGameAsync(id);
            return Results.Json(result);
        });
    }
}