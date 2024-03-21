using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Serilog;

namespace CrpgP.Application;

public class PortraitService
{
    private readonly IPortraitRepository _repository;
    private readonly ILogger _logger;
    private readonly ICacheService _cacheService;
    
    public PortraitService(
        IPortraitRepository repository,
        ICacheService cacheService,
        ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task<Result> GetPortraitByIdAsync(int id)
    {
        var portrait = await _cacheService.GetOrFetchEntryAsync($"portrait-{id}", () => _repository.FindByIdAsync(id));
        return portrait is null 
            ? Result.Failure(PortraitErrors.NotFound(id))
            : Result.Success(portrait);
    }

    public async Task<Result> GetPortraitsByIdsAsync(IEnumerable<int> ids)
    {
        var portraitIds = ids as int[] ?? ids.ToArray();
        var portraits =
            await _cacheService.GetOrFetchEntryAsync($"portraits-ids{portraitIds}", () => _repository.FindByIdsAsync(portraitIds));
        
        return portraits != null && portraits.Count > 0
            ? Result.Success(portraits)
            : Result.Failure(PortraitErrors.NoneFound(portraitIds));
    }
    
    public async Task<Result> GetPortraitsCount()
    { 
        var count = await _repository.CountAll();

        return count == 0 
            ? Result.Failure(PortraitErrors.CountIsZero())
            : Result.Success(count);
    }

    public async Task<Result> GetPortraitsPage(int page, int count)
    {
        var portraits = await _cacheService.GetOrFetchEntryAsync($"portraits-pg{page}c{count}", () => _repository.FindAllPage(page, count));
        return portraits is null
            ? Result.Failure(PortraitErrors.PagePortraitsNotFound(page))
            : Result.Success(portraits);
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
            _cacheService.RemoveEntry($"portrait-{portrait.Id}");
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
            _cacheService.RemoveEntry($"portrait-{id}");
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