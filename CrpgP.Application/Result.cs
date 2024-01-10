using CrpgP.Domain.Errors;

namespace CrpgP.Application;

public class Result
{
    private Result(bool isSuccess, Error error, object? value)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error ", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }
    
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public object? Value { get; private set; }
    
    public static Result Success() => new(true, Error.None, default);
    public static Result Success(object value) => new(true, Error.None, value);
    public static Result Failure(Error error) => new(false, error, default);
}