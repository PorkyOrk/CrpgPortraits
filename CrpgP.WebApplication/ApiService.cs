using System.Text.Json;

namespace CrpgP.WebApplication;

public class ApiService
{
    private readonly HttpClient _httpClient;
    
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<T?> GetByIdAsync<T>(int id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5100/api/v1/game?id={id}"); // TODO configure base URL
        response.EnsureSuccessStatusCode();
        
        var responseBody = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        using var jsonDocument = JsonDocument.Parse(responseBody);
        // jsonDocument.RootElement.TryGetProperty("value", out var value);
        // var deserialized = value.Deserialize<T>(options);

        var myClass = jsonDocument.RootElement.GetProperty("value").Deserialize<T>(options);
        
        return myClass;


        // TODO Add validation and logging

    }
}
