namespace CrpgP.WebApi.Endpoints;

public static class EndpointsSetup
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGameEndpoints();
        app.MapPortraitEndpoints();
        app.MapSizeEndpoints();
        app.MapTagEndpoints();
        app.MapHealthChecks("/health");
    }
}