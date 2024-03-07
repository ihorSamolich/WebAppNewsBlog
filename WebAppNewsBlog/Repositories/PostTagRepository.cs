using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Interfaces.Repository;

namespace WebAppNewsBlog.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private readonly AppEFContext _context;

        public PostTagRepository(AppEFContext context)
        {
            _context = context;
        }
        public PostTagMapEntity Add(PostTagMapEntity entity)
        {
            _context.PostTags.Add(entity);
            return entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PostTagMapEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostTagMapEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public PostTagMapEntity GetBySlug(string slug)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public PostTagMapEntity Update(PostTagMapEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
