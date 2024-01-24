namespace CrpgP.Application.Result;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string[]? Messages { get; private set; }

    private Result()
    {
    }
    
    public static Result<T> Success()
    {
        return new Result<T>
        {
            IsSuccess = true,
        };
    }
    
    public static Result<T> Success(T value)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Value = value
        };
    }

    public static Result<T> Failure(params string[] errors)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Messages = errors
        };
    }
}