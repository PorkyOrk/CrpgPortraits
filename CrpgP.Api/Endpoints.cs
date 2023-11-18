namespace CrpgP.Api;

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
        // **** Portraits ******
        // Get portrait by Id
        // Get portraits by ids[]
        // Get portraits[] by tag Id
        
        
        // *********************
        // ******* Tags ********
        // Get tag by Id
        // Get tag by Name
        // Get tags[] by portrait id
        
        

        
        
        
        
    }
}