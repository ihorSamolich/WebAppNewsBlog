using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Interfaces.Repository;

namespace WebAppNewsBlog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppEFContext _context;

        public TagRepository(AppEFContext context)
        {
            _context = context;
        }
        public TagEntity Add(TagEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _context.Tags.Count();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TagEntity> GetAll()
        {
            return _context.Tags
                .OrderBy(t => t.Name);
        }

        public TagEntity GetById(int id)
        {
            return
                _context.Tags
                     .Where(p => p.Id == id)
                     .SingleOrDefault();
        }
        public TagEntity GetBySlug(string slug)
        {
            return
                _context.Tags
                     .Where(p => p.UrlSlug == slug)
                     .SingleOrDefault();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }

        public TagEntity Update(TagEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
