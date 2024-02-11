using System.Text.Json;
using CrpgP.WebApplication.Models;

namespace CrpgP.WebApplication.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly Dictionary<Type, string> _typeSlugMap;
    
    public ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration.GetSection("BackendApi:BaseUrl").Value ?? throw new ArgumentException("Missing Backend Url in configuration.");
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _typeSlugMap = new Dictionary<Type, string>
        {
            { typeof(GameModel), "game" },
            { typeof(PortraitModel), "portrait" },
            { typeof(SizeModel), "size" },
            { typeof(TagModel), "tag" }
        };
    }

    private string GetSlugFromModelType<T>()
    {
        if (_typeSlugMap.TryGetValue(typeof(T), out var value))
        {
            return value;
        }
        throw new ArgumentException("No slug found for the specified type.");
    }
    
    private async Task<T?> GetRequest<T>(string uri)
    {
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        
        using var jsonDocument = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        var result = jsonDocument.RootElement.GetProperty("value").Deserialize<T>(_jsonSerializerOptions);
        return result;
    }
    
    public async Task<T?> GetByIdAsync<T>(int id)
    {
        var uri = $"{_baseUrl}/api/v1/{GetSlugFromModelType<T>()}?id={id}";
        return await GetRequest<T>(uri);
    }
    
    public async Task<T?> GetAll<T>()
    {
        var uri = $"{_baseUrl}/api/v1/{GetSlugFromModelType<T>()}/all";
        return await GetRequest<T>(uri);
    }
}
