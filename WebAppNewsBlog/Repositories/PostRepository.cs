using Microsoft.EntityFrameworkCore;
using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Interfaces.Repository;

namespace WebAppNewsBlog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppEFContext _context;

        public PostRepository(AppEFContext context)
        {
            _context = context;
        }

        public PostEntity Add(PostEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _context.Posts.Count();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PostEntity> GetAll()
        {
            return
                _context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.PostTags)
                        .ThenInclude(t => t.Tag)
                    .OrderByDescending(p => p.PostedOn);
        }

        public PostEntity GetById(int id)
        {
            return
               _context.Posts
                    .Where(p => p.Id == id)
                    .SingleOrDefault();
        }
        public PostEntity GetBySlug(string slug)
        {
            return
                _context.Posts
                     .Include(p => p.Category)
                     .Include(p => p.PostTags)
                        .ThenInclude(t => t.Tag)
                     .Where(p => p.UrlSlug == slug)
                     .SingleOrDefault();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public PostEntity Update(PostEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
