using CrpgP.Application.Result;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace CrpgP.Application;

public class SizeService
{
    public bool CacheEnabled { get; set; }
    public int CacheEntryExpireSeconds { get; set; } = 60;
    
    private readonly ISizeRepository _repository;
    private readonly ILogger _logger;
    private readonly CacheHelper<Size> _cacheHelper;
    
    public SizeService(ISizeRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheHelper = new CacheHelper<Size>(cache, CacheEntryExpireSeconds);
    }
    
    public async Task<Result<Size>> GetSizeByIdAsync(int id)
    {
        var size = await _cacheHelper.GetEntryFromCacheOrRepository(id, () => _repository.FindByIdAsync(id));
        return size is null 
            ? Result<Size>.Failure($"Size with id: {id} was not found.")
            : Result<Size>.Success(size);
    }

    public async Task<Result<IEnumerable<Size>>> GetSizeByDimensionsAsync(int width, int height)
    {
        var sizes = await _repository.FindByDimensionsAsync(width, height);
        return sizes is null 
            ? Result<IEnumerable<Size>>.Failure($"No sizes with dimensions x:{width} , y:{height} were found.")
            : Result<IEnumerable<Size>>.Success(sizes);
    }

    public async Task<Result<int>> CreateSizeAsync(string json)
    {
        try
        {
            var size = Validation.Mapper.MapToType<Size>(json);
            var result = await _repository.InsertAsync(size);
            return Result<int>.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Size create failed. {0}", e.Message);
            return Result<int>.Failure(e.Message);
        }
    }

    public async Task<Result<object>> UpdateSizeAsync(string json)
    {
        try
        {
            var size = Validation.Mapper.MapToType<Size>(json);
            await _repository.UpdateAsync(size);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Size update failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }
    
    public async Task<Result<object>> DeleteSizeAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Size delete failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }

}