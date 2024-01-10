using CrpgP.Application.Options;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;

namespace CrpgP.Application;

public class SizeService
{
    private readonly ISizeRepository _repository;
    private readonly ILogger _logger;
    private readonly bool _cacheEnabled;
    private readonly CacheHelper<Size> _cacheHelper;
    
    public SizeService(IOptions<MemoryCacheOptions> memoryCacheOptions, ISizeRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheEnabled = memoryCacheOptions.Value.Enabled;
        _cacheHelper = new CacheHelper<Size>(cache, memoryCacheOptions.Value.EntryExpiryInSeconds);
    }
    
    public async Task<Result> GetSizeByIdAsync(int id)
    {
        var size = _cacheEnabled 
            ? await _cacheHelper.GetEntryFromCacheOrRepository(id, () => _repository.FindByIdAsync(id))
            : await _repository.FindByIdAsync(id);
        return size is null 
            ? Result.Failure(SizeErrors.NotFound(id))
            : Result.Success(size);
    }

    public async Task<Result> GetSizeByDimensionsAsync(int width, int height)
    {
        var sizes = await _repository.FindByDimensionsAsync(width, height);
        return sizes is null 
            ? Result.Failure(SizeErrors.NotFoundByDimensions(width,height))
            : Result.Success(sizes);
    }

    public async Task<Result> CreateSizeAsync(Size size)
    {
        try
        {
            var result = await _repository.InsertAsync(size);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Size create failed. {0}", e.Message);
            return Result.Failure(SizeErrors.CreateFailed());
        }
    }

    public async Task<Result> UpdateSizeAsync(Size size)
    {
        try
        {
            await _repository.UpdateAsync(size);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Size update failed. {0}", e.Message);
            return Result.Failure(SizeErrors.UpdateFailed());
        }
    }
    
    public async Task<Result> DeleteSizeAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Size delete failed. {0}", e.Message);
            return Result.Failure(SizeErrors.DeleteFailed());
        }
    }

}