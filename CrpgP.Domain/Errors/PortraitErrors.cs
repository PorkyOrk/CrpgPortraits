namespace CrpgP.Domain.Errors;

public static class PortraitErrors
{
    public static Error NotFound(int portraitId)
    {
        return Error.NotFound("Portrait.NotFound", $"The portrait with id = '{portraitId}' was not found");
    }
    
    public static Error NoneFound(IEnumerable<int> portraitId)
    {
        return Error.NotFound("Portrait.NoneFound", $"No portraits with the specified ids were found were not found. ids = '{portraitId}'");
    }
    
    public static Error CreateFailed()
    {
        return Error.Failure("Portrait.CreateFailed", "Create of the portrait failed.");
    }
    
    public static Error UpdateFailed()
    {
        return Error.Failure("Portrait.UpdateFailed", "Update of the portrait failed.");
    }
    
    public static Error DeleteFailed()
    {
        return Error.Failure("Portrait.DeleteFailed", "Delete of the portrait failed.");
    }
}