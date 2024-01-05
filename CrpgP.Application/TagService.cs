using CrpgP.Application.Options;
using CrpgP.Application.Result;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;

namespace CrpgP.Application;

public class TagService
{
    private readonly ITagRepository _repository;
    private readonly ILogger _logger;
    private readonly bool _cacheEnabled;
    private readonly CacheHelper<Tag> _cacheHelper;

    public TagService(
        IOptions<MemoryCacheOptions> memoryCacheOptions, ITagRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheEnabled = memoryCacheOptions.Value.Enabled;
        _cacheHelper = new CacheHelper<Tag>(cache, memoryCacheOptions.Value.EntryExpiryInSeconds);
    }
    
    public async Task<Result<Tag>> GetTagByIdAsync(int id)
    {
        var tag = _cacheEnabled
            ? await _cacheHelper.GetEntryFromCacheOrRepository(id, () => _repository.FindByIdAsync(id))
            : await _repository.FindByIdAsync(id);
        return tag is null 
            ? Result<Tag>.Failure($"Tag with id: {id} was not found.")
            : Result<Tag>.Success(tag);
    }
    
    public async Task<Result<Tag>> GetTagByNameAsync(string name)
    {
        var tag = _cacheEnabled
        ? await _cacheHelper.GetEntryFromCacheOrRepository(name, () => _repository.FindByNameAsync(name))
        : await _repository.FindByNameAsync(name);
        return tag is null
            ? Result<Tag>.Failure($"Tag with name: {name} was not found.")
            : Result<Tag>.Success(tag);
    }
    
    public async Task<Result<int>> CreateTagAsync(string json)
    {
        try
        {
            var tag = Validation.Mapper.MapToType<Tag>(json);
            var result = await _repository.InsertAsync(tag);
            _logger.Information("New tag created. id: {0}", tag.Id);
            return Result<int>.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Tag create failed. {0}", e.Message);
            return Result<int>.Failure(e.Message);
        }
    }

    public async Task<Result<object>> DeleteTagAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            _logger.Information("Tag successfully deleted.");
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Tag delete failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }
}