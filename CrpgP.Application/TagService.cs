using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public class TagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public Tag FindTagById(int tagId)
    {
        return _repository.GetById(tagId);
    }
    
    public Tag FindTagByName(string tagName)
    {
        return _repository.GetByName(tagName);
    }
    
    public void InsertTag(Tag tag)
    {
        _repository.Insert(tag);
    }

    public void UpdateTag(Tag tag)
    {
        _repository.Update(tag);
    }
    
    public void DeleteTag(int tagId)
    {
        _repository.Delete(tagId);
    }
}