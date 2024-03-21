using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Serilog;

namespace CrpgP.Application;

public class SizeService
{
    private readonly ISizeRepository _repository;
    private readonly ILogger _logger;
    private readonly ICacheService _cacheService;


    public SizeService(ISizeRepository repository, ICacheService cacheService, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task<Result> GetSizeByIdAsync(int id)
    {
        var size = await _cacheService.GetOrFetchEntryAsync($"size-{id}", () => _repository.FindByIdAsync(id));
        return size is null 
            ? Result.Failure(SizeErrors.NotFound(id))
            : Result.Success(size);
    }

    public async Task<Result> GetSizeByDimensionsAsync(int width, int height)
    {
        var sizes = await _cacheService.GetOrFetchEntryAsync($"sizes-w{width}h{height}",
            () => _repository.FindByDimensionsAsync(width, height));
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
            _cacheService.RemoveEntry($"size-{size.Id}");
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
            _cacheService.RemoveEntry($"size-{id}");
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