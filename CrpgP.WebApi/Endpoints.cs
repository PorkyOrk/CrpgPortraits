namespace CrpgP.WebApi;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        
        // ********************
        //Example Endpoints 
        // ********************
        app.MapGet("/",() => "Hello World");
        app.MapGet("/myendpoint", () => "This is my endpoint!");
        
        // Does not show in swagger 
        app.MapGet("/endone", async context =>
        {
            await context.Response.WriteAsJsonAsync(new { Message = "This is endpoint one" });
        });
        // Does not show in swagger
        app.MapGet("/endtwo", async context =>
        {
            await context.Response.WriteAsJsonAsync(new { Message = "This is endpoint two" });
        });
        // =====================================================
        
        
        // *********************
        // ******* Game ********
        // Get game by Id
        // app.MapGet("/game/{id}", (
        //     HttpContext context,
        //     [FromRoute] int id,
        //     [FromServices] IConfiguration config) =>
        // {
        //     var x = new SearchGame(new GameRepository(config));
        //     var g = x.FindById(id);
        //
        //     context.Response.WriteAsync(g.Name);
        // });
        
        
        
        
        // Get game by Name



        // ********************
        // **** Portrait ******
        // Get portrait by Id
        // Get portraits by ids[]
        // Get portraits[] by tag Id


        // ********************
        // ******* Tag ********
        // Get tag by Id
        // Get tag by Name
        // Get tags[] by portrait id







    }
}