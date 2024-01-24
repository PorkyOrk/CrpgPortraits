using System.Text.Json;
using CrpgP.Application.Exceptions;

namespace CrpgP.Application.Validation;

[Obsolete]
public static class Mapper
{
    public static T MapToType<T>(string json)
    {
        var result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default);
        return result ?? throw new JsonPayloadDeserializationException($"Unable to map payload to type {typeof(T).Name}.");
    }
}