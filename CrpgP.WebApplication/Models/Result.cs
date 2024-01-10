namespace CrpgP.WebApplication.Models;

internal sealed class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public string[]? Messages { get; init; }
}