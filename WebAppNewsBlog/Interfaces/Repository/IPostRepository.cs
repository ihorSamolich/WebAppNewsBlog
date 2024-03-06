using WebAppNewsBlog.Data.Entities;

namespace WebAppNewsBlog.Interfaces.Repository
{
    public interface IPostRepository : IRepository<PostEntity>
    {
        IQueryable<PostEntity> GetLatest(int count);
        IQueryable<PostEntity> GetByCategory(string slug);
        IQueryable<PostEntity> GetByTags(string slug);
    }
}
