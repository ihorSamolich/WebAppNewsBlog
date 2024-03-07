using WebAppNewsBlog.Data.Entities;

namespace WebAppNewsBlog.Interfaces.Repository
{
    public interface ITagRepository : IRepository<TagEntity>
    {
        TagEntity GetByName(string name);

    }
}
