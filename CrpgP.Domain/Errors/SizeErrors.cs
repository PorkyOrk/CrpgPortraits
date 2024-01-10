namespace CrpgP.Domain.Errors;

public static class SizeErrors
{
    public static Error NotFound(int sizeId)
    {
        return Error.NotFound("Size.NotFound", $"The size with id = '{sizeId}' was not found");
    }
    
    public static Error NotFoundByDimensions(int width,  int height)
    {
        return Error.NotFound("Size.NotFoundByDimensions", $"No size found with width='{width}' and height='{height}'");
    }
    
    public static Error CreateFailed()
    {
        return Error.Failure("Size.CreateFailed", "Create of the size failed.");
    }
    
    public static Error UpdateFailed()
    {
        return Error.Failure("Size.UpdateFailed", "Update of the size failed.");
    }
    
    public static Error DeleteFailed()
    {
        return Error.Failure("Size.DeleteFailed", "Delete of the size failed.");
    }
}