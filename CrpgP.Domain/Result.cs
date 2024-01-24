using System.Text.Json.Serialization;
using CrpgP.Domain.Errors;

namespace CrpgP.Domain;

public class Result
{
    [JsonIgnore]
    public bool IsSuccess { get; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? Error { get; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; private set; }
    
    private Result(bool isSuccess, Error? error, object? value)
    {
        if (isSuccess && error != null || !isSuccess && error == null)
        {
            throw new ArgumentException("Invalid error ", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }
    
    public static Result Success() => new(true, null, default);
    public static Result Success(object value) => new(true, null, value);
    public static Result Failure(Error error) => new(false, error, default);
}