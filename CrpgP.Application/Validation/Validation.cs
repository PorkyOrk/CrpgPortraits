using System.Text.Json;
using CrpgP.Application.Exceptions;

namespace CrpgP.Application.Validation;


// Todo refactor the validation

public static class Validation
{
    public static void RequestInput(string? payload)
    {
        // Check for empty payload
        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new PayloadEmptyException("Empty payload.");
        }

        
        // Note: Add more validation here...
    }
    
    public static T MapToType<T>(string json)
    {
        var result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default);
        return result ?? throw new JsonPayloadDeserializationException("Unable to map payload to Game object.");
    }
}