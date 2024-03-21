using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Serilog;

namespace CrpgP.Application;

public class TagService
{
    private readonly ITagRepository _repository;
    private readonly ILogger _logger;
    private readonly ICacheService _cacheService;

    public TagService(ITagRepository repository, ICacheService cacheService, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task<Result> GetTagByIdAsync(int id)
    {
        var tag = await _cacheService.GetOrFetchEntryAsync($"tag-{id}", () => _repository.FindByIdAsync(id));
        return tag is null 
            ? Result.Failure(TagErrors.NotFound(id))
            : Result.Success(tag);
    }
    
    public async Task<Result> GetTagByNameAsync(string name)
    {
        var tag = await _cacheService.GetOrFetchEntryAsync($"tag-{name}", () => _repository.FindByNameAsync(name));
        return tag is null
            ? Result.Failure(TagErrors.NotFoundByName(name))
            : Result.Success(tag);
    }
    
    public async Task<Result> CreateTagAsync(Tag tag)
    {
        try
        {
            var result = await _repository.InsertAsync(tag);
            _logger.Information("New tag created. id: {0}", tag.Id);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Tag create failed. {0}", e.Message);
            return Result.Failure(TagErrors.CreateFailed());
        }
    }

    public async Task<Result> DeleteTagAsync(int id)
    {
        try
        {
            _cacheService.RemoveEntry($"tag-{id}");
            await _repository.DeleteAsync(id);
            _logger.Information("Tag successfully deleted.");
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Tag delete failed. {0}", e.Message);
            return Result.Failure(TagErrors.DeleteFailed());
        }
    }
}