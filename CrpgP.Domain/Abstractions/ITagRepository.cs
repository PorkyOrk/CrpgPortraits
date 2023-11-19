using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface ITagRepository
{
    public Tag GetById(int tagId);
    public Tag GetByName(string tagName);
    public void Insert(Tag tag);
    public void Update(Tag tag);
    public void Delete(int tagId);
}