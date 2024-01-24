using CrpgP.Application.Options;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
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
    
    public async Task<Result> GetPortraitByIdAsync(int id)
    {
        var portrait = _cacheEnabled 
            ? await _cacheHelper.GetEntryFromCacheOrRepository(id,() => _repository.FindByIdAsync(id))
            : await _repository.FindByIdAsync(id);
        return portrait is null 
            ? Result.Failure(PortraitErrors.NotFound(id))
            : Result.Success(portrait);
    }

    public async Task<Result> GetPortraitsByIdsAsync(IEnumerable<int> ids)
    {
        var portraitIds = ids as int[] ?? ids.ToArray();
        var portraits = await _repository.FindByIdsAsync(portraitIds);
        return portraits.Count > 0
            ? Result.Success(portraits)
            : Result.Failure(PortraitErrors.NoneFound(portraitIds));
    }

    public async Task<Result> CreatePortraitAsync(Portrait portrait)
    {
        try
        {
            var result = await _repository.InsertAsync(portrait);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Portrait create failed. {0}", e.Message);
            return Result.Failure(PortraitErrors.CreateFailed());
        }
    }

    public async Task<Result> UpdatePortraitAsync(Portrait portrait)
    {
        try
        {
            await _repository.UpdateAsync(portrait);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Portrait update failed. {0}", e.Message);
            return Result.Failure(PortraitErrors.UpdateFailed());
        }
    }
    
    public async Task<Result> DeletePortraitAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Portrait delete failed. {0}", e.Message);
            return Result.Failure(PortraitErrors.DeleteFailed());
        }
    }

}