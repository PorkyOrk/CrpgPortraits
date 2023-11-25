using CrpgP.Application;

namespace CrpgP.WebApi.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var handler = ActivatorUtilities.CreateInstance<GameHandler>(app.Services);
        
        app.MapGet("api/v1/game/{id}", async Task<IResult>(int id) =>
        {
            // TODO: Instead of a try catch block the handler method should return a result object 
            
            try
            {
                var result = await handler.FindGameByIdAsync(id);
                return Results.Json(result);
            }
            catch (Exception e)
            {
                return Handle(e);
            }
        });
        
        app.MapGet("api/v1/game/find/", async Task<IResult> (HttpRequest request) =>
        {
            try
            {
                var name = request.Query["name"].ToString();
                var result = await handler.FindGameByNameAsync(name);
                return Results.Json(result);
            }
            catch (Exception e)
            {
                return Handle(e);
            }
        });
        
        app.MapDelete("api/v1/game/delete/{id}", async Task<IResult>(int id) =>
        {
            try
            {
                await handler.DeleteGameAsync(id);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Handle(e);
            }
        });
        
        app.MapPost("api/v1/game/create/", async Task<IResult>(HttpContext context) =>
        {
            try
            {
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                await handler.InsertGameAsync(body);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Handle(e);
            }
        });
        
        app.MapPut("api/v1/game/update/", async Task<IResult> (HttpContext context) =>
        {
            try
            {
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                await handler.UpdateGameAsync(body);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Handle(e);
            }
        });
    }

    
    
    private static IResult Handle(Exception e)
    {
        //TODO
        
        Console.WriteLine(e.Message); 
        return Results.Empty; 
    }
}