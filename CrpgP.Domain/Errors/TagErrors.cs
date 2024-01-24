namespace CrpgP.Domain.Errors;

public static class TagErrors
{
    public static Error NotFound(int tagId)
    {
        return Error.NotFound("Tag.NotFound", $"The tag with id = '{tagId}' was not found");
    }
    
    public static Error NotFoundByName(string tagName)
    {
        return Error.NotFound("Tag.NotFoundByName", $"The tag with name = '{tagName}' was not found");
    }
    
    public static Error CreateFailed()
    {
        return Error.Failure("Tag.CreateFailed", "Create of the tag failed.");
    }
    
    public static Error DeleteFailed()
    {
        return Error.Failure("Tag.DeleteFailed", "Delete of the tag failed.");
    }
}