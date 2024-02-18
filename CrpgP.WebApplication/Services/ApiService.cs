using System.Collections;
using System.Collections.ObjectModel;
using System.Text.Json;
using CrpgP.WebApplication.Models;
using Microsoft.VisualBasic;

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
    
    public async Task<IEnumerable<T>?> GetAll<T>()
    {
        var uri = $"{_baseUrl}/api/v1/{GetSlugFromModelType<T>()}/all";
        return await GetRequest<IEnumerable<T>?>(uri);
    }
    
    
    
    
    private async Task<IEnumerable<int>?> GetAllIds<T>()
    {
        var uri = $"{_baseUrl}/api/v1/{GetSlugFromModelType<T>()}/ids";
        return await GetRequest<IEnumerable<int>?>(uri);
    }
    
    // TODO: Consider refactor to its own, non-generic class
    public async Task<IEnumerable<GameModel>> GetAllGames()
    {
        var ids = await GetAllIds<GameModel>();
        if (ids == null)
        {
            // TODO Throw exception
            throw new NullReferenceException("No game ids found.");
        }

        var games = new Collection<GameModel>();
        
        foreach (var id in ids)
        {
            var game = await GetByIdAsync<GameModel>(id);
            if (game != null)
            {
                games.Add(game);
            }
        }

        if (games.Count == 0 )
        {
            //TODO Log error, maybe throw exception
        }
        
        return games;
    }
    
    
}
