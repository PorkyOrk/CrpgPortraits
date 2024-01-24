namespace CrpgP.Domain.Errors;

public static class GameErrors
{
    public static Error NotFound(int gameId)
    {
        return Error.NotFound("Game.NotFound", $"The game with id = '{gameId}' was not found");
    }
    
    public static Error NotFoundByName(string gameName)
    {
        return Error.NotFound("Game.NotFoundByName", $"The game with name = '{gameName}' was not found");
    }

    public static Error CreateFailed()
    {
        return Error.Failure("Game.CreateFailed", "Create of the game failed.");
    }
    
    public static Error UpdateFailed()
    {
        return Error.Failure("Game.UpdateFailed", "Update of the game failed.");
    }
    
    public static Error DeleteFailed()
    {
        return Error.Failure("Game.DeleteFailed", "Delete of the game failed.");
    }
}