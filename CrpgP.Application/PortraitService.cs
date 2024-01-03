using CrpgP.Application.Options;
using CrpgP.Application.Result;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;

namespace CrpgP.Application;

public class PortraitService
{
    private readonly IPortraitRepository _repository;
    private readonly ILogger _logger;
    private readonly bool _cacheEnabled;
    private readonly CacheHelper<Portrait> _cacheHelper;
    
    public PortraitService(IOptions<MemoryCacheOptions> memoryCacheOptions, IPortraitRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheEnabled = memoryCacheOptions.Value.Enabled;
        _cacheHelper = new CacheHelper<Portrait>(cache, memoryCacheOptions.Value.EntryExpiryInSeconds);
    }
    
    public async Task<Result<Portrait>> GetPortraitById(int id)
    {
        var portrait = _cacheEnabled 
            ? await _cacheHelper.GetEntryFromCacheOrRepository(id,() => _repository.FindByIdAsync(id))
            : await _repository.FindByIdAsync(id);
        return portrait is null 
            ? Result<Portrait>.Failure($"Portrait with id: {id} was not found.")
            : Result<Portrait>.Success(portrait);
    }

    public async Task<Result<Dictionary<int,Portrait?>>> GetPortraitsByIds(IEnumerable<int> ids)
    {
        // NOTE: This is not using the cache
        var portraits = await _repository.FindByIdsAsync(ids);
        return Result<Dictionary<int,Portrait?>>.Success(portraits);
    }

    public async Task<Result<int>> CreatePortrait(string json)
    {
        try
        {
            var portrait = Validation.Mapper.MapToType<Portrait>(json);
            var result = await _repository.InsertAsync(portrait);
            return Result<int>.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Portrait create failed. {0}", e.Message);
            return Result<int>.Failure(e.Message);
        }
    }

    public async Task<Result<object>> UpdatePortrait(string json)
    {
        try
        {
            var portrait = Validation.Mapper.MapToType<Portrait>(json);
            await _repository.UpdateAsync(portrait);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Portrait update failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }
    
    public async Task<Result<object>> DeletePortrait(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Portrait delete failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }

}